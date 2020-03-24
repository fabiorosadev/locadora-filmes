/* eslint-disable react/forbid-prop-types */
import React, { useState, useEffect } from 'react';
import Container from '@material-ui/core/Container';
import CssBaseline from '@material-ui/core/CssBaseline';
import PropTypes from 'prop-types';
import Table from '../../components/Table';
import {
  getRentals,
  deleteRental,
  deleteRentals,
} from '../../services/Rentals/rentalService';
import useStyles from './styles';

export default function Rentals({ history }) {
  const classes = useStyles();
  const [selected, setSelected] = useState([]);
  const [rows, setRows] = useState([]);

  const headCells = [
    {
      id: 'customerCpf',
      numeric: false,
      disablePadding: true,
      label: 'CPF Cliente',
    },
    {
      id: 'movies',
      numeric: false,
      disablePadding: true,
      label: 'Filmes',
    },
    {
      id: 'rentalDate',
      numeric: false,
      disablePadding: true,
      label: 'Data Locação',
    },
  ];

  async function loadRentals() {
    const { data } = await getRentals();
    const rentals = data.map(r => {
      return {
        id: r.id,
        customerCpf: r.customerCpf,
        movies: r.movieRentals.map(mr => `${mr.movie.name}, `),
        rentalDate: new Date(r.rentalDate).toLocaleDateString(),
      };
    });
    setRows(rentals);
  }

  useEffect(() => {
    loadRentals();
  }, []);

  const handleAdd = () => {
    history.push('/rentals/0');
  };

  const handleEdit = () => {
    const rentalId = selected[0];
    history.push(`/rentals/${rentalId}`);
  };

  const handleDelete = async () => {
    if (selected.length > 1) {
      await deleteRentals(selected);
    } else {
      const rentalId = selected[0];
      await deleteRental(rentalId);
    }
    await loadRentals();
    setSelected([]);
  };

  return (
    <Container component="main">
      <CssBaseline />
      <div className={classes.paper}>
        <Table
          title="Locações"
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

Rentals.defaultProps = {
  history: null,
};

Rentals.propTypes = {
  history: PropTypes.any,
};
