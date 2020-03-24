/* eslint-disable no-param-reassign */
/* eslint-disable jsx-a11y/anchor-is-valid */
/* eslint-disable react/forbid-prop-types */
import React, { useRef, useEffect, useState } from 'react';
import { Form } from '@unform/web';
import * as Yup from 'yup';
import Button from '@material-ui/core/Button';
import Container from '@material-ui/core/Container';
import CssBaseline from '@material-ui/core/CssBaseline';
import Typography from '@material-ui/core/Typography';
import MomentUtils from '@date-io/moment';
import {
  MuiPickersUtilsProvider,
  KeyboardDatePicker,
} from '@material-ui/pickers';
import { TextField } from 'unform-material-ui';
import FormControl from '@material-ui/core/FormControl';
import PropTypes from 'prop-types';
import { toast } from 'react-toastify';
import MovieListDialog from './RentalFormMovieList';
import Table from '../../components/Table';
import { getRental, saveRental } from '../../services/Rentals/rentalService';
import { getMovies } from '../../services/Movies/movieService';
import useStyles from './styles';

export default function RentalForm({ history, match }) {
  const classes = useStyles();

  const formRef = useRef(null);

  const [rental, setRental] = useState({
    id: 0,
    customerCpf: '',
    rentalDate: new Date().toISOString(),
  });

  const [movies, setMovies] = useState([]);
  const [rentalMovies, setRentalMovies] = useState([]);
  const [selectedDate, setSelectedDate] = useState(new Date());
  const [selectedMovies, setSelectedMovies] = useState([]);

  const schema = Yup.object().shape({
    customerCpf: Yup.string().required('CPF é obrigatório!'),
  });

  const handleSubmit = async data => {
    try {
      // Remove all previous errors
      formRef.current.setErrors({});
      await schema.validate(data, {
        abortEarly: false,
      });
      // Validation passed
      data.id = rental.id;
      data.rentalDate = selectedDate;
      if (rentalMovies.length <= 0) {
        toast.error('Pelo menos um filme deve ser adicionado!');
        return;
      }
      data.movieRentals = rentalMovies.map(mr => {
        return {
          movieId: mr.id,
          rentalId: rental.id,
        };
      });
      await saveRental(data);
      history.push('/rentals');
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

  const movieHeadCells = [
    {
      id: 'name',
      numeric: false,
      disablePadding: true,
      label: 'Nome',
    },
    {
      id: 'genre',
      numeric: false,
      disablePadding: true,
      label: 'Gênero',
    },
  ];

  const handleCancel = () => {
    history.push('/rentals');
  };

  const handleSelectedDate = date => {
    setSelectedDate(date);
  };

  const mapToMoviesListViewModel = moviesList => {
    return moviesList.map(m => {
      return {
        id: m.id,
        name: m.name,
        genre: m.genre.name,
        creationDate: new Date(m.creationDate).toLocaleDateString(),
        status: m.status === 1 ? 'Ativo' : 'Inativo',
      };
    });
  };

  const handleDeleteMovie = async () => {
    const rentalMoviesIds = rentalMovies.map(r => r.id);
    const originalMovies = movies.filter(
      m => rentalMoviesIds.indexOf(m.id) !== -1
    );
    const newRentalMovies = originalMovies.filter(
      rm => selectedMovies.indexOf(rm.id) === -1
    );
    setRentalMovies(mapToMoviesListViewModel(newRentalMovies));
    setSelectedMovies([]);
  };

  const handleSelectMovie = moviesSelectedIds => {
    const originalMoviesSelected = movies.filter(
      m => moviesSelectedIds.indexOf(m.id) !== -1
    );

    setRentalMovies(mapToMoviesListViewModel(originalMoviesSelected));
    setSelectedMovies([]);
  };

  useEffect(() => {
    const loadData = async () => {
      const { data: allMovies } = await getMovies();
      setMovies(allMovies);
      const { id } = match.params;
      if (id > 0) {
        const { data } = await getRental(id);
        const rentalMoviesIds = data.movieRentals.map(mr => mr.movieId);
        const actualRentalOriginalMovies = allMovies.filter(
          m => rentalMoviesIds.indexOf(m.id) !== -1
        );
        formRef.current.setData(data);
        setRental(data);
        setRentalMovies(mapToMoviesListViewModel(actualRentalOriginalMovies));
      } else {
        formRef.current.setData(rental);
      }
    };
    loadData();
  }, []);

  return (
    <Container component="main">
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
            name="customerCpf"
            variant="outlined"
            margin="normal"
            fullWidth
            label="CPF Cliente"
            autoComplete="customrCpf"
            autoFocus
          />
          <FormControl variant="outlined" className={classes.formControl}>
            <MuiPickersUtilsProvider utils={MomentUtils}>
              <KeyboardDatePicker
                disableToolbar
                variant="inline"
                format="DD/MM/YYYY"
                margin="normal"
                label="Data Locação"
                value={selectedDate}
                onChange={handleSelectedDate}
                KeyboardButtonProps={{
                  lang: 'pt-BR',
                }}
              />
            </MuiPickersUtilsProvider>
          </FormControl>
          <MovieListDialog
            onConfirm={handleSelectMovie}
            selectedItems={rentalMovies.map(r => r.id)}
          />
          <Table
            title="Filmes"
            headCells={movieHeadCells}
            rows={rentalMovies}
            selected={selectedMovies}
            setSelected={setSelectedMovies}
            defaultSortedProperty="name"
            onDelete={handleDeleteMovie}
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

RentalForm.defaultProps = {
  history: null,
  match: null,
};

RentalForm.propTypes = {
  history: PropTypes.any,
  match: PropTypes.any,
};
