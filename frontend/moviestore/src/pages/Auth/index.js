/* eslint-disable jsx-a11y/anchor-is-valid */
/* eslint-disable react/forbid-prop-types */
import React, { useRef, useEffect } from 'react';
import { Form } from '@unform/web';
import * as Yup from 'yup';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import Container from '@material-ui/core/Container';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import CssBaseline from '@material-ui/core/CssBaseline';
import Typography from '@material-ui/core/Typography';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import { TextField, Checkbox } from 'unform-material-ui';
import Grid from '@material-ui/core/Grid';
import Link from '@material-ui/core/Link';
import PropTypes from 'prop-types';
import { login, getCurrentUser } from '../../services/Auth/authService';
import useStyles from './styles';

export default function Auth({ history, location }) {
  const classes = useStyles();

  const formRef = useRef(null);

  const schema = Yup.object().shape({
    userName: Yup.string().required('Usuário é obrigatório!'),
    password: Yup.string().required('Senha é obrigatória!'),
  });

  const handleRegistration = () => {
    history.push('/registration');
  };

  const handleSubmit = async data => {
    try {
      // Remove all previous errors
      formRef.current.setErrors({});
      await schema.validate(data, {
        abortEarly: false,
      });
      // Validation passed
      await login(data.userName, data.password);
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

  useEffect(() => {
    async function loadUser() {
      if (await getCurrentUser()) {
        history.push('/');
      }
    }

    loadUser();
  });

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Login
        </Typography>
        <Form
          ref={formRef}
          onSubmit={handleSubmit}
          className={classes.form}
          noValidate
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
          <FormControlLabel
            control={
              <Checkbox name="remember" value="remember" color="primary" />
            }
            label="Lembrar"
          />
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
          >
            Entrar
          </Button>
          <Grid container>
            <Grid item xs>
              {/* <Link href="#" variant="body2">
                Esqueceu a senha?
              </Link> */}
            </Grid>
            <Grid item>
              <Link variant="body2" onClick={handleRegistration}>
                Não tem cadastro? Registre-se
              </Link>
            </Grid>
          </Grid>
        </Form>
      </div>
    </Container>
  );
}

Auth.defaultProps = {
  history: null,
  location: null,
};

Auth.propTypes = {
  history: PropTypes.any,
  location: PropTypes.any,
};
