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
      margin: -12,
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

type Props = {
   business: BusinessDTO;
   data?: BusinessDataDTO;
   primaryButtonAction?: (id: number, name: string) => void;
   secondaryButtonAction?: (id: number, name: string) => void;
   secondaryButtonText?: string;
   primaryButtonText?: string;
};

export const ExtendedBusinessCard: React.FC<Props> = ({
   primaryButtonAction,
   primaryButtonText,
   business,
   data,
   secondaryButtonAction,
   secondaryButtonText,
}: Props) => {
   const styles = useStyles();
   const history = useHistory();

   return (
      <Card
         className={styles.card}
         //buttonAction={() => secondaryButtonAction(business.id, business.name)}
         buttonColor="secondary"
         buttonText={secondaryButtonText}
         buttonSize="medium"
         title="Business Data"
         variant="outlined"
      >
         <div className={styles.card}>
            <CardRow
               text="Name"
               data={business.name}
               buttonText="Edit"
               buttonAction={() => console.log}
            />
            <CardRow
               text="Type"
               data={business.type}
               buttonText="Edit"
               buttonAction={() => console.log}
            />
            <CardRow
               text="Zip"
               data={business.zip}
               buttonText="Edit"
               buttonAction={() => console.log}
            />

            <CardRow
               text="Capacity"
               data={business.capacity}
               buttonText="Edit"
               buttonAction={() => console.log}
            />

            <CardRow
               text="Business Hours"
               data={business.businessHours!}
               buttonText="Edit"
               buttonAction={() => console.log}
            />

            <CardRow
               text="Visit Length"
               data={business.timeSlotLength}
               buttonText="Edit"
               buttonAction={() => console.log}
            />
            <CardRow
               text="Employees"
               data={0}
               buttonText="Manage"
               buttonAction={() => console.log}
            />

            <CardRow
               text="Bookings"
               data={0}
               buttonText="Manage"
               buttonAction={() =>
                  history.push('/business/bookings', {
                     data: {id: business.id, name: business.name},
                  })
               }
            />

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
