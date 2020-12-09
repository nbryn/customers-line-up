import React, {useState} from 'react';
import {Container} from 'react-bootstrap';
import CssBaseline from '@material-ui/core/CssBaseline';
import {makeStyles} from '@material-ui/core/styles';

import {LoginView} from './user/LoginView';
import {MainMenu} from '../components/MainMenu';
import {Routes} from './Routes';
import {useUserContext} from '../context/UserContext';

const marginTop = 10;
const marginBottom = 4;

const useStyles = makeStyles((theme) => ({
   root: {},
   content: {
      marginTop: theme.spacing(marginTop),
      marginBottom: theme.spacing(marginBottom),
   },
}));

export const MainView: React.FC = () => {
   const styles = useStyles();
   const [mobileOpen, setMobileOpen] = useState(false);
   const {userLoggedIn} = useUserContext();

   const handleMenuClose = () => {
      setMobileOpen(false);
   };

   return (
      <div className={styles.root}>
         <CssBaseline />
         {!userLoggedIn ? (
            <LoginView />
         ) : (
            <>
               <MainMenu mobileOpen={mobileOpen} onClose={handleMenuClose} />
               <Container className={styles.root}>
                  <Routes />
               </Container>
            </>
         )}
      </div>
   );
};
