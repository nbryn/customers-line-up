import React, {useState} from 'react';
import {Container} from 'react-bootstrap';
import CssBaseline from '@material-ui/core/CssBaseline';
import {makeStyles} from '@material-ui/core/styles';

import {LoginView} from '../features/user/LoginView';
import {MainMenu} from '../common/components/navigation/MainMenu';
import {Routes} from './Routes';
import {useUserContext} from '../features/user/UserContext';

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
   const {user} = useUserContext();

   const handleMenuClose = () => {
      setMobileOpen(false);
   };

   return (
      <div className={styles.root}>
         <CssBaseline />
         {!user.email ? (
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
