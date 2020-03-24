/* eslint-disable react/forbid-prop-types */
import React, { useEffect } from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import PropTypes from 'prop-types';
import Movies from '../../Movies';

export default function RentalFormMovieList({ onConfirm, selectedItems }) {
  const [open, setOpen] = React.useState(false);
  const [selected, setSelected] = React.useState([]);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleConfirm = () => {
    onConfirm(selected);
    setOpen(false);
  };

  const handleClose = () => {
    setOpen(false);
  };

  useEffect(() => {
    setSelected(selectedItems);
  }, [selectedItems]);

  return (
    <>
      <Button variant="outlined" color="primary" onClick={handleClickOpen}>
        Selecionar Filmes
      </Button>
      <Dialog open={open} maxWidth="lg">
        <DialogTitle id="max-width-dialog-title">Lista de Filmes</DialogTitle>
        <DialogContent>
          <Movies
            moviesSelected={selected}
            onSelectMovie={setSelected}
            allowCrud={false}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleConfirm} color="primary">
            Confirmar
          </Button>
          <Button onClick={handleClose} color="primary">
            Close
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
}

RentalFormMovieList.propTypes = {
  onConfirm: PropTypes.func.isRequired,
  selectedItems: PropTypes.array.isRequired,
};
