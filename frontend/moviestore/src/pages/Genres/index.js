/* eslint-disable react/forbid-prop-types */
import React, { useState, useEffect } from 'react';
import Container from '@material-ui/core/Container';
import CssBaseline from '@material-ui/core/CssBaseline';
import PropTypes from 'prop-types';
import Table from '../../components/Table';
import {
  getGenres,
  deleteGenre,
  deleteGenres,
} from '../../services/Genres/genreService';
import useStyles from './styles';

export default function Genres({ history }) {
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
      id: 'creationDate',
      numeric: false,
      disablePadding: true,
      label: 'Data Criação',
    },
    { id: 'status', numeric: false, disablePadding: true, label: 'Status' },
  ];

  async function loadGenres() {
    const { data } = await getGenres();
    const genres = data.map(g => {
      return {
        id: g.id,
        name: g.name,
        creationDate: new Date(g.creationDate).toLocaleDateString(),
        status: g.status === 1 ? 'Ativo' : 'Inativo',
      };
    });
    setRows(genres);
  }

  useEffect(() => {
    loadGenres();
  }, []);

  const handleAdd = () => {
    history.push('/genres/0');
  };

  const handleEdit = () => {
    const genreId = selected[0];
    history.push(`/genres/${genreId}`);
  };

  const handleDelete = async () => {
    if (selected.length > 1) {
      await deleteGenres(selected);
    } else {
      const genre = selected[0];
      await deleteGenre(genre);
    }
    setSelected([]);
    await loadGenres();
  };

  return (
    <Container component="main">
      <CssBaseline />
      <div className={classes.paper}>
        <Table
          title="Gêneros"
          headCells={headCells}
          rows={rows}
          selected={selected}
          setSelected={setSelected}
          defaultSortedProperty="name"
          onAdd={handleAdd}
          onEdit={handleEdit}
          onDelete={handleDelete}
        />
      </div>
    </Container>
  );
}

Genres.defaultProps = {
  history: null,
};

Genres.propTypes = {
  history: PropTypes.any,
};
