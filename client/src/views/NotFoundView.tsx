import React from 'react';
import {Link} from 'react-router-dom';
import {makeStyles} from '@material-ui/core/styles';

import PageNotFound from '../assets/images/PageNotFound.png';

const useStyles = makeStyles((theme) => ({
   link: {
      textAlign: 'center',
      marginTop: 10
   },
}));

export const NotFoundView: React.FC = () => {
   const classes = useStyles();
   return (
      <>
         <img
            src={PageNotFound}
            style={{
               width: 500,
               height: 500,
               display: 'block',
               margin: 'auto',
               position: 'relative',
            }}
         />
         <div className={classes.link}>
            <Link className={classes.link} to="/">
               Return to Home
            </Link>
         </div>
      </>
   );
};
