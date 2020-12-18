import React from 'react';
import {makeStyles} from '@material-ui/core/styles';

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
   buttonAction?: () => void;
   title: string;
   buttonText?: string;
};

export const ExtendedCard: React.FC<Props> = ({buttonAction, buttonText, title, data}: Props) => {
   const styles = useStyles();

   return (
      <Card
         className={styles.card}
         buttonAction={() => buttonAction!()}
         buttonColor="secondary"
         buttonText={buttonText}
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
