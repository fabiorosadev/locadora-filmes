/* eslint-disable react/forbid-prop-types */
/* eslint-disable react/jsx-props-no-spreading */
import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import PropTypes from 'prop-types';
import { getCurrentUser } from '../../services/Auth/authService';

const ProtectedRoute = ({ path, component: Component, render, ...rest }) => {
  return (
    <Route
      path={path}
      {...rest}
      render={props => {
        if (!getCurrentUser()) return <Redirect to="/auth" />;
        return Component ? <Component {...props} /> : render(props);
      }}
    />
  );
};

export default ProtectedRoute;

ProtectedRoute.propTypes = {
  path: PropTypes.string.isRequired,
  component: PropTypes.any,
  render: PropTypes.func,
  rest: PropTypes.any,
};

ProtectedRoute.defaultProps = {
  component: null,
  render: null,
  rest: null,
};
