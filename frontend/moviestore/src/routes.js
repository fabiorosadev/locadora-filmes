import React from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import ProtectedRoute from './components/ProtectedRoute';
import Auth from './pages/Auth';
import Registration from './pages/Auth/Registration';
import Genres from './pages/Genres';
import GenreFrom from './pages/GenreForm';
import Movies from './pages/Movies';
import MovieForm from './pages/MovieForm';
import Rentals from './pages/Rentals';
import RentalForm from './pages/RentalForm';

export default function Routes() {
  return (
    <Switch>
      <Route path="/auth" component={Auth} exact />
      <Route path="/registration" component={Registration} exact />
      <ProtectedRoute path="/genres" component={Genres} exact />
      <ProtectedRoute path="/genres/:id" component={GenreFrom} exact />
      <ProtectedRoute path="/movies" component={Movies} exact />
      <ProtectedRoute path="/movies/:id" component={MovieForm} exact />
      <ProtectedRoute path="/rentals" component={Rentals} exact />
      <ProtectedRoute path="/rentals/:id" component={RentalForm} exact />
      <Redirect from="/" exact to="/movies" />
    </Switch>
  );
}
