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
import Checkbox from '@material-ui/core/Checkbox';
import PropTypes from 'prop-types';
import { getGenre, saveGenre } from '../../services/Genres/genreService';
import useStyles from './styles';

export default function GenreForm({ history, match }) {
  const classes = useStyles();

  const formRef = useRef(null);

  const [genre, setGenre] = useState({});
  const [status, setStatus] = useState(true);

  const schema = Yup.object().shape({
    name: Yup.string().required('Nome é obrigatório!'),
  });

  const handleSubmit = async data => {
    try {
      // Remove all previous errors
      formRef.current.setErrors({
        id: 0,
        name: '',
        status: 1,
        creationDate: new Date().toISOString(),
      });
      await schema.validate(data, {
        abortEarly: false,
      });
      // Validation passed
      genre.name = data.name;
      genre.status = status ? 1 : 0;
      await saveGenre(genre);
      history.push('/genres');
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

  const handleCancel = () => {
    history.push('/genres');
  };

  useEffect(() => {
    const loadGenre = async () => {
      const { id } = match.params;
      if (id > 0) {
        const { data } = await getGenre(id);
        formRef.current.setData(data);
        setStatus(data.status === 1);
        setGenre(data);
      } else {
        formRef.current.setData(genre);
      }
    };
    loadGenre();
  }, []);

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Typography component="h1" variant="h5">
          Gênero
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

GenreForm.defaultProps = {
  history: null,
  match: null,
};

GenreForm.propTypes = {
  history: PropTypes.any,
  match: PropTypes.any,
};
