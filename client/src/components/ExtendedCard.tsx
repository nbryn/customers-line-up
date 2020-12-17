import React, {useState} from 'react';
import {makeStyles} from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Divider from '@material-ui/core/Divider';
import Typography from '@material-ui/core/Typography';
import {useHistory} from 'react-router-dom';

import {BusinessDTO, BusinessDataDTO} from '../models/dto/Business';
import {Card} from './Card';
import {CardRow} from './CardRow';

const useStyles = makeStyles({
   root: {
      minWidth: 275,
   },
   card: {
      textAlign: 'left',
   },
   typography: {
      marginTop: 30,
   },
   mix: {
      marginTop: 15,
      marginBottom: 15,
   },
   center: {
      textAlign: 'center',
      marginTop: -25,
   },
   button: {
      textAlign: 'right',
      marginTop: -25,
   },
   primaryButton: {
      justifyContent: 'center',
      marginTop: 50,
      marginBottom: -10,
   },
});

export type ExtendedCardData = {
   text: string;
   data: string | number | undefined;
   buttonText: string;
   buttonAction: () => void;
};

type Props = {
   data: ExtendedCardData[];
   buttonAction?: (id: number, name: string) => void;

   title: string;
};

export const ExtendedCard: React.FC<Props> = ({buttonAction, title, data}: Props) => {
   const styles = useStyles();

   return (
      <Card
         className={styles.card}
         //buttonAction={() => secondaryButtonAction(business.id, business.name)}
         buttonColor="secondary"
         buttonText=""
         buttonSize="medium"
         title={title}
         variant="outlined"
      >
         {data.map((x) => (
            <CardRow
               text={x.text}
               data={x.data}
               buttonText={x.buttonText}
               buttonAction={x.buttonAction}
            />
         ))}
         <div className={styles.card}>
            {/* <Button
               className={styles.primaryButton}
               variant="contained"
               color="primary"
               onClick={() => primaryButtonAction(business.id, business.name)}
               size="medium"
            >
               {primaryButtonText}
            </Button> */}
         </div>
      </Card>
   );
};
