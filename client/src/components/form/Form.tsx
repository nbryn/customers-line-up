import React, {FormEvent} from 'react';
import {Alert} from 'react-bootstrap';
import Button from '@material-ui/core/Button';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';


const useStyles = makeStyles((theme) => ({
   alert: {
      display: 'inline-block',
      marginTop: -20,
      marginBottom: 40,
      maxWidth: 380,
   },
   paper: {
      marginTop: theme.spacing(8),
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
   },
   form: {
      marginTop: theme.spacing(1),
   },
   button: {
      marginTop: 30,
      width: '38%'
   },
   working: {
      transform: 'translateX(90%)',
      top: 50,
   },
}));

type Props = {
   children?: React.ReactNode;
   buttonText: string;
   image?: number;
   errorMessage?: string;
   valid: boolean;
   working: boolean;
   style?: any;
   onSubmit: (e: FormEvent<HTMLFormElement>) => void;
};

export const Form: React.FC<Props> = ({
   children,
   buttonText,
   valid,
   working,
   errorMessage,
   style,
   onSubmit,
}) => {
   const styles = useStyles();
   return (
      <>
         <form style={style} noValidate onSubmit={onSubmit}>
            {errorMessage && (
               <Alert className={styles.alert} variant="danger">
                  {errorMessage}
               </Alert>
            )}

            {working ? (
               <CircularProgress className={styles.working} />
            ) : (
               <>
                  {children}
                  <Button
                     className={styles.button}
                     disabled={!valid}
                     color="primary"
                     size="medium"
                     type="submit"
                     variant="contained"
                  >
                     {buttonText}
                  </Button>
               </>
            )}
         </form>
      </>
   );
};
