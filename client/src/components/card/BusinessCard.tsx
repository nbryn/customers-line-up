import React from 'react';
import {makeStyles} from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';

import {BusinessDTO} from '../../models/Business';
import {Card} from './Card';

const useStyles = makeStyles({
   root: {
      minWidth: 275,
   },
   card: {
      textAlign: 'center',
      marginBottom: 15,
   },
   primaryButton: {
      justifyContent: 'center',
      marginTop: 50,
      marginBottom: -10,
   },
});

type Props = {
   business: BusinessDTO;
   buttonAction: () => void;
   buttonText?: string;
};

export const BusinessCard: React.FC<Props> = ({buttonAction, buttonText, business}: Props) => {
   const styles = useStyles();

   return (
      <Card
         className={styles.card}
         buttonAction={() => buttonAction()}
         buttonColor="primary"
         buttonText={buttonText}
         buttonSize="medium"
         title={business.name}
         subTitle={business.type}
         variant="outlined"
      >
         <div className={styles.card}>
            <Typography>Zip: {business.zip} </Typography>
            <Typography>Capacity: {business.capacity} </Typography>
            <Typography>Business Hours: {business.opens + ' - ' + business.closes} </Typography>
         </div>
      </Card>
   );
};
