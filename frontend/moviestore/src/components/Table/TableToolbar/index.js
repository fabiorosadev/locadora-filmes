import React from 'react';
import PropTypes from 'prop-types';
import clsx from 'clsx';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import Tooltip from '@material-ui/core/Tooltip';
import DeleteIcon from '@material-ui/icons/Delete';
import AddIcon from '@material-ui/icons/Add';
import EditIcon from '@material-ui/icons/Edit';
import useToolbarStyles from './styles';

export default function TableToolbar({
  numSelected,
  title,
  onAdd,
  onEdit,
  onDelete,
}) {
  const classes = useToolbarStyles();

  return (
    <Toolbar
      className={clsx(classes.root, {
        [classes.highlight]: numSelected > 0,
      })}
    >
      {numSelected > 0 ? (
        <Typography
          className={classes.title}
          color="inherit"
          variant="subtitle1"
        >
          {numSelected} selecionados
        </Typography>
      ) : (
        <Typography className={classes.title} variant="h6" id="tableTitle">
          {title}
        </Typography>
      )}
      {onEdit && numSelected > 0 && numSelected === 1 && (
        <Tooltip title="Alterar">
          <IconButton aria-label="edit" onClick={onEdit}>
            <EditIcon />
          </IconButton>
        </Tooltip>
      )}
      {onDelete && numSelected > 0 ? (
        <Tooltip title="Excluir">
          <IconButton aria-label="delete" onClick={onDelete}>
            <DeleteIcon />
          </IconButton>
        </Tooltip>
      ) : (
        onAdd && (
          <Tooltip title="Incluir">
            <IconButton aria-label="add" onClick={onAdd}>
              <AddIcon />
            </IconButton>
          </Tooltip>
        )
      )}
    </Toolbar>
  );
}

TableToolbar.defaultProps = {
  onAdd: null,
  onEdit: null,
  onDelete: null,
};

TableToolbar.propTypes = {
  numSelected: PropTypes.number.isRequired,
  title: PropTypes.string.isRequired,
  onAdd: PropTypes.func,
  onEdit: PropTypes.func,
  onDelete: PropTypes.func,
};
