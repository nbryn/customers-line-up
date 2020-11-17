import React, {useState} from 'react';
import {Container, Grid} from '@material-ui/core';
import CssBaseline from '@material-ui/core/CssBaseline';
import {makeStyles} from '@material-ui/core/styles';

import {LoginView} from './LoginView';
import {Routes} from './Routes';

const marginTop = 10;
const marginBottom = 4;

const useStyles = makeStyles((theme) => ({
   content: {
      marginTop: theme.spacing(marginTop),
      marginBottom: theme.spacing(marginBottom),
   },
   wrapper: {
      textAlign: 'center',
   },
}));

export const MainView = () => {
   const styles = useStyles();

   const [mobileOpen, setMobileOpen] = useState(false);

   const handleMenuClose = () => {
      setMobileOpen(false);
   };

   return (
      <Container>
         <LoginView />
      </Container>
   );
};
