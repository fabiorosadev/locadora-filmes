/* eslint-disable no-param-reassign */
/* eslint-disable jsx-a11y/anchor-is-valid */
/* eslint-disable react/forbid-prop-types */
import React, { useRef, useEffect, useState } from 'react';
import { Form } from '@unform/web';
import * as Yup from 'yup';
import Button from '@material-ui/core/Button';
import Container from '@material-ui/core/Container';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import CssBaseline from '@material-ui/core/CssBaseline';
import Typography from '@material-ui/core/Typography';
import { TextField } from 'unform-material-ui';
import Select from '@material-ui/core/Select';
import FormControl from '@material-ui/core/FormControl';
import FormHelperText from '@material-ui/core/FormHelperText';
import InputLabel from '@material-ui/core/InputLabel';
import Checkbox from '@material-ui/core/Checkbox';
import MenuItem from '@material-ui/core/MenuItem';
import PropTypes from 'prop-types';
import { getMovie, saveMovie } from '../../services/Movies/movieService';
import { getGenres } from '../../services/Genres/genreService';
import useStyles from './styles';

export default function MovieForm({ history, match }) {
  const classes = useStyles();

  const formRef = useRef(null);

  const [movie, setMovie] = useState({
    id: 0,
    name: '',
    genreId: 0,
    status: 1,
    creationDate: new Date().toISOString(),
  });

  const [selectedGenre, setSelectedGenre] = useState({
    value: -1,
    error: '',
  });

  const [genres, setGenres] = useState([
    {
      id: -1,
      name: '(Não Informado)',
    },
  ]);
  const [status, setStatus] = useState(true);

  const schema = Yup.object().shape({
    name: Yup.string().required('Nome é obrigatório!'),
    genreId: Yup.number().moreThan(0, 'Gênero é obrigatório!'),
  });

  const handleSubmit = async data => {
    try {
      // Remove all previous errors
      if (
        genres.map(g => g.id).indexOf(selectedGenre.value) === -1 ||
        selectedGenre.value < 0
      ) {
        const { value } = selectedGenre;
        setSelectedGenre({ value, error: 'Gênero é obrigatório!' });
      }
      formRef.current.setErrors({});
      await schema.validate(data, {
        abortEarly: false,
      });
      // Validation passed
      data.genreId = selectedGenre.value;
      movie.name = data.name;
      movie.genreId = data.genreId;
      delete movie.genre;
      movie.status = status ? 1 : 0;
      await saveMovie(movie);
      history.push('/movies');
    } catch (err) {
      const validationErrors = {};
      if (err instanceof Yup.ValidationError) {
        err.inner.forEach(error => {
          validationErrors[error.path] = error.message;
        });
        formRef.current.setErrors(validationErrors);
      }
    }
  };

  const handleChangeStatus = event => {
    setStatus(event.target.checked);
  };

  const handleGenreSelect = event => {
    setSelectedGenre({ value: event.target.value, error: '' });
  };

  const handleCancel = () => {
    history.push('/movies');
  };

  useEffect(() => {
    const loadData = async () => {
      const { data: allGenres } = await getGenres();
      const nullGenre = {
        id: -1,
        name: '(Não Informado)',
      };
      setGenres([nullGenre, ...allGenres]);
      if (allGenres.length > 0) {
        setSelectedGenre({ value: allGenres[0].id });
      }
      const { id } = match.params;
      if (id > 0) {
        const { data } = await getMovie(id);
        formRef.current.setData(data);
        setStatus(data.status === 1);
        setSelectedGenre({ value: data.genreId });
        setMovie(data);
      } else {
        formRef.current.setData(movie);
      }
    };
    loadData();
  }, []);

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Typography component="h1" variant="h5">
          Filme
        </Typography>
        <Form
          ref={formRef}
          onSubmit={handleSubmit}
          className={classes.form}
          noValidate
        >
          <TextField
            name="name"
            variant="outlined"
            margin="normal"
            fullWidth
            label="Nome"
            autoComplete="name"
            autoFocus
          />
          <FormControl
            variant="outlined"
            className={classes.formControl}
            error={!!selectedGenre.error}
          >
            <InputLabel id="genreId-select-label">Gênero</InputLabel>
            <Select
              labelId="genreId-select-label"
              label="Gênero"
              displayEmpty={false}
              style={{ width: '100%' }}
              value={selectedGenre.value}
              error={!!selectedGenre.error}
              onChange={handleGenreSelect}
            >
              {genres.map(g => (
                <MenuItem key={g.id} value={g.id}>
                  {g.name}
                </MenuItem>
              ))}
            </Select>
            <FormHelperText>{selectedGenre.error}</FormHelperText>
          </FormControl>
          <FormControlLabel
            control={
              <Checkbox
                name="status"
                color="primary"
                checked={status}
                onChange={handleChangeStatus}
              />
            }
            label="Ativo"
          />
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
          >
            Salvar
          </Button>
          <Button
            type="button"
            fullWidth
            variant="contained"
            color="secondary"
            onClick={handleCancel}
          >
            Cancelar
          </Button>
        </Form>
      </div>
    </Container>
  );
}

MovieForm.defaultProps = {
  history: null,
  match: null,
};

MovieForm.propTypes = {
  history: PropTypes.any,
  match: PropTypes.any,
};
