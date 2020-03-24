import React, { useState, useEffect } from 'react';
import { ToastContainer } from 'react-toastify';
import Container from '@material-ui/core/Container';
import NavDrawer from './components/NavDrawer';
import useNavbarStyles from './components/NavDrawer/styles';
import Routes from './routes';
import { getCurrentUser } from './services/Auth/authService';
import 'react-toastify/dist/ReactToastify.css';

function App() {
  const navbarClasses = useNavbarStyles();

  const [user, setUser] = useState();
  const [padding, setPagging] = useState(0);

  useEffect(() => {
    async function loadUser() {
      const currentUser = await getCurrentUser();
      setUser(currentUser);
    }

    loadUser();
  }, []);

  const handleDrawerOpen = isOpen => {
    if (isOpen) setPagging(240);
    else setPagging(0);
  };

  return (
    <>
      <ToastContainer />
      <NavDrawer user={user} onOpen={handleDrawerOpen} />
      <Container
        className={navbarClasses.content}
        style={{ paddingLeft: padding }}
      >
        <div className={navbarClasses.toolbar}>
          <Routes user={user} />
        </div>
      </Container>
    </>
  );
}

export default App;
