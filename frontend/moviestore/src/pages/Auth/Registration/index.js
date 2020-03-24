/* eslint-disable jsx-a11y/anchor-is-valid */
/* eslint-disable react/forbid-prop-types */
import React, { useRef } from 'react';
import { Form } from '@unform/web';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import Link from '@material-ui/core/Link';
import Grid from '@material-ui/core/Grid';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';
import { TextField } from 'unform-material-ui';
import * as Yup from 'yup';
import PropTypes from 'prop-types';
import { register } from '../../../services/Auth/authService';
import useStyles from './styles';

export default function Registration({ history, location }) {
  const classes = useStyles();

  const formRef = useRef(null);

  const schema = Yup.object().shape({
    userName: Yup.string().required('Usuário é obrigatório!'),
    password: Yup.string().required('Senha é obrigatória!'),
    confirmPassword: Yup.string()
      .required('Confirme a senha novamente!')
      .oneOf([Yup.ref('password')], 'Senhas não são iguais!'),
  });

  const handleLogin = () => {
    history.push('/auth');
  };

  const handleSubmit = async data => {
    try {
      // Remove all previous errors
      formRef.current.setErrors({});
      await schema.validate(data, {
        abortEarly: false,
      });
      // Validation passed
      await register(data.userName, data.password);
      const { state } = location;
      window.location = state ? state.from.pathname : '/';
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

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Criar Conta
        </Typography>
        <Form
          ref={formRef}
          className={classes.form}
          noValidate
          onSubmit={handleSubmit}
        >
          <TextField
            name="userName"
            type="userName"
            variant="outlined"
            margin="normal"
            fullWidth
            label="Usuário"
            autoComplete="userName"
            autoFocus
          />
          <TextField
            name="password"
            type="password"
            variant="outlined"
            margin="normal"
            fullWidth
            label="Senha"
            autoComplete="current-password"
            autoFocus
          />
          <TextField
            name="confirmPassword"
            type="password"
            variant="outlined"
            margin="normal"
            fullWidth
            label="Repetir Senha"
            autoComplete="current-password"
            autoFocus
          />
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
          >
            Registrar
          </Button>
          <Grid container justify="flex-end">
            <Grid item>
              <Link variant="body2" onClick={handleLogin}>
                Ja possui cadastro? Login
              </Link>
            </Grid>
          </Grid>
        </Form>
      </div>
    </Container>
  );
}

Registration.defaultProps = {
  history: null,
  location: null,
};

Registration.propTypes = {
  history: PropTypes.any,
  location: PropTypes.any,
};
