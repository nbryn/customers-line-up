import React, {useState} from 'react';
import {makeStyles} from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Divider from '@material-ui/core/Divider';
import Typography from '@material-ui/core/Typography';
import {useHistory} from 'react-router-dom';

import {BusinessDTO, BusinessDataDTO} from '../models/dto/Business';
import {Card} from './Card';

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
         title={business.name}
         subTitle={business.type}
         variant="outlined"
      >
         <div className={styles.card}>
            <div className={styles.typography}>
               <Divider />
               <Typography className={styles.mix}>Zip: {business.zip} </Typography>

               <Divider />
            </div>
            <Typography className={styles.typography}>Capacity: {business.capacity} </Typography>
            <Divider />
            <Typography className={styles.typography}>
               Business Hours: {business.businessHours}{' '}
            </Typography>
            <Divider />
            <Typography className={styles.typography}>
               Visit Length: {business.timeSlotLength} minutes{' '}
            </Typography>
            <Divider />
            <Typography className={styles.typography}>Employees </Typography>
            <Divider />
            <Typography className={styles.typography}>
               Bookings:
               <div className={styles.center}>{0}</div>
               <div className={styles.button}>
                  <Button
                     size="small"
                     variant="contained"
                     onClick={() =>
                        history.push('/business/bookings', {
                           data: {id: business.id, name: business.name},
                        })
                     }
                  >
                     Manage Bookings
                  </Button>
               </div>
            </Typography>
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
