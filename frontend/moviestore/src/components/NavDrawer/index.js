/* eslint-disable react/forbid-prop-types */
import React, { useState } from 'react';
import clsx from 'clsx';
import { Link } from 'react-router-dom';
import { useTheme } from '@material-ui/core/styles';
import { Link as MaterialLink } from '@material-ui/core';
import Drawer from '@material-ui/core/Drawer';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import MovieIcon from '@material-ui/icons/Movie';
import GenreIcon from '@material-ui/icons/Style';
import RentalIcon from '@material-ui/icons/Store';
import AccountCircleIcon from '@material-ui/icons/AccountCircle';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import MenuItem from '@material-ui/core/MenuItem';
import Menu from '@material-ui/core/Menu';
import PropTypes from 'prop-types';
import useStyles from './styles';
import { logout } from '../../services/Auth/authService';

export default function NavDrawer({ user, onOpen }) {
  const classes = useStyles();
  const theme = useTheme();
  const [open, setOpen] = useState(false);
  const [anchorEl, setAnchorEl] = useState(null);

  const menuOpen = Boolean(anchorEl);

  const handleDrawerOpen = () => {
    setOpen(true);
    onOpen(true);
  };

  const handleDrawerClose = () => {
    setOpen(false);
    onOpen(false);
  };

  const handleAccountMenu = event => {
    setAnchorEl(event.currentTarget);
  };

  const handleAccountClose = () => {
    setAnchorEl(null);
  };

  const handleLogout = () => {
    logout();
    window.location = '/auth';
  };

  return (
    <div className={classes.root}>
      <AppBar
        position="fixed"
        className={clsx(classes.appBar, { [classes.appBarShift]: open })}
      >
        <Toolbar>
          <IconButton
            color="inherit"
            aria-label="open drawer"
            onClick={handleDrawerOpen}
            edge="start"
            className={clsx(classes.menuButton, { [classes.hide]: open })}
          >
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" noWrap className={classes.title}>
            Locadora
          </Typography>
          {!user && (
            <Link to="/auth" className={classes.link}>
              <IconButton
                aria-label="login"
                aria-controls="menu-appbar"
                aria-haspopup="true"
                color="inherit"
                title="Entrar"
              >
                <ExitToAppIcon />
              </IconButton>
            </Link>
          )}
          {user && (
            <>
              <Typography variant="subtitle2" noWrap>
                {`Olá ${user.unique_name}`}
              </Typography>
              <div>
                <IconButton
                  aria-label="account of current user"
                  aria-controls="menu-appbar"
                  aria-haspopup="true"
                  onClick={handleAccountMenu}
                  color="inherit"
                >
                  <AccountCircleIcon />
                </IconButton>
                <Menu
                  id="menu-appbar"
                  anchorEl={anchorEl}
                  anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  keepMounted
                  transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  open={menuOpen}
                  onClose={handleAccountClose}
                >
                  <MaterialLink to="/logout" style={{ textDecoration: 'none' }}>
                    <MenuItem onClick={handleLogout}>Sair</MenuItem>
                  </MaterialLink>
                </Menu>
              </div>
            </>
          )}
        </Toolbar>
      </AppBar>
      <Drawer
        variant="permanent"
        className={clsx(classes.drawer, {
          [classes.drawerOpen]: open,
          [classes.drawerClose]: !open,
        })}
        classes={{
          paper: clsx({
            [classes.drawerOpen]: open,
            [classes.drawerClose]: !open,
          }),
        }}
      >
        <div className={classes.toolbar}>
          <IconButton onClick={handleDrawerClose}>
            {theme.direction === 'rtl' ? (
              <ChevronRightIcon />
            ) : (
              <ChevronLeftIcon />
            )}
          </IconButton>
        </div>
        <Divider />
        <List>
          <Link to="/genres" className={classes.link}>
            <ListItem button key="genres">
              <ListItemIcon>
                <GenreIcon />
              </ListItemIcon>
              <ListItemText primary="Gêneros" />
            </ListItem>
          </Link>
          <Link to="/movies" className={classes.link}>
            <ListItem button key="movies">
              <ListItemIcon>
                <MovieIcon />
              </ListItemIcon>
              <ListItemText primary="Filmes" />
            </ListItem>
          </Link>
          <Link to="/rentals" className={classes.link}>
            <ListItem button key="rentals">
              <ListItemIcon>
                <RentalIcon />
              </ListItemIcon>
              <ListItemText primary="Locações" />
            </ListItem>
          </Link>
        </List>
      </Drawer>
    </div>
  );
}

NavDrawer.defaultProps = {
  user: null,
  onOpen: null,
};

NavDrawer.propTypes = {
  user: PropTypes.object,
  onOpen: PropTypes.func,
};
