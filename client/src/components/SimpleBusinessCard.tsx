import React, {useState} from 'react';
import {makeStyles} from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';

import {Card} from './Card';

const useStyles = makeStyles({
   root: {
      minWidth: 275,
   },
   card: {
      textAlign: 'center',
   },
   primaryButton: {
      justifyContent: 'center',
      marginTop: 50,
      marginBottom: -10,
   },
});

type BusinessInfo = {
   id: number;
   name: string;
   type: string;
   zip: string;
   capacity: number;
   businessHours: string;
};

type Props = {
   data: BusinessInfo;
   buttonAction: (id: number, name: string) => void;
   buttonText?: string;
};

export const SimpleBusinessCard: React.FC<Props> = ({buttonAction, buttonText, data}: Props) => {
   const styles = useStyles();

   return (
      <Card
         className={styles.card}
         buttonAction={() => buttonAction(data.id, data.name)}
         buttonColor="secondary"
         buttonText={buttonText}
         buttonSize="medium"
         title={data.name}
         subTitle={data.type}
         variant="outlined"
      >
         <div className={styles.card}>
            <Typography>Zip: {data.zip} </Typography>
            <Typography>Capacity: {data.capacity} </Typography>
            <Typography>Business Hours: {data.businessHours} </Typography>
         </div>
      </Card>
   );
};
