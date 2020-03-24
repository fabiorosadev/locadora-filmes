/* eslint-disable react/forbid-prop-types */
import React, { useState, useEffect } from 'react';
import Container from '@material-ui/core/Container';
import CssBaseline from '@material-ui/core/CssBaseline';
import PropTypes from 'prop-types';
import Table from '../../components/Table';
import {
  getMovies,
  deleteMovie,
  deleteMovies,
} from '../../services/Movies/movieService';
import useStyles from './styles';

export default function Movies({
  history,
  moviesSelected,
  onSelectMovie,
  allowCrud,
}) {
  const classes = useStyles();
  const [selected, setSelected] = useState([]);
  const [rows, setRows] = useState([]);

  const headCells = [
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
    {
      id: 'creationDate',
      numeric: false,
      disablePadding: true,
      label: 'Data Criação',
    },
    { id: 'status', numeric: false, disablePadding: true, label: 'Status' },
  ];

  async function loadMovies() {
    const { data } = await getMovies();
    const movies = data.map(m => {
      return {
        id: m.id,
        name: m.name,
        genre: m.genre.name,
        creationDate: new Date(m.creationDate).toLocaleDateString(),
        status: m.status === 1 ? 'Ativo' : 'Inativo',
      };
    });
    setRows(movies);
  }

  const handleAdd = () => {
    history.push('/movies/0');
  };

  const handleEdit = () => {
    const movieId = selected[0];
    history.push(`/movies/${movieId}`);
  };

  const handleSelect = arrSelected => {
    setSelected(arrSelected);
    if (onSelectMovie) {
      onSelectMovie(arrSelected);
    }
  };

  useEffect(() => {
    loadMovies();
    if (moviesSelected) {
      handleSelect(moviesSelected);
    }
  }, [moviesSelected]);

  const handleDelete = async () => {
    if (selected.length > 1) {
      await deleteMovies(selected);
    } else {
      const movieId = selected[0];
      await deleteMovie(movieId);
    }
    setSelected([]);
    await loadMovies();
  };

  return (
    <Container component="main">
      <CssBaseline />
      <div className={classes.paper}>
        <Table
          title="Filmes"
          headCells={headCells}
          rows={rows}
          selected={selected}
          setSelected={handleSelect}
          defaultSortedProperty="name"
          onAdd={(allowCrud && handleAdd) || null}
          onEdit={(allowCrud && handleEdit) || null}
          onDelete={(allowCrud && handleDelete) || null}
        />
      </div>
    </Container>
  );
}

Movies.defaultProps = {
  history: null,
  moviesSelected: null,
  onSelectMovie: null,
  allowCrud: true,
};

Movies.propTypes = {
  history: PropTypes.any,
  moviesSelected: PropTypes.array,
  onSelectMovie: PropTypes.func,
  allowCrud: PropTypes.bool,
};
