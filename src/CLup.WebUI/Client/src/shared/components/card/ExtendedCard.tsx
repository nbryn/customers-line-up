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
   end: {
      marginTop: -20,
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
         buttonAction={buttonAction || undefined}
         buttonColor="secondary"
         buttonText={buttonText}
         buttonSize="medium"
         title={title}
         variant="outlined"
      >
         {data.map((x) => (
            <CardRow
               key={x.text}
               text={x.text}
               data={x.data}
               buttonText={x.buttonText}
               buttonAction={x.buttonAction}
            />
         ))}
         <div className={styles.end}></div>
      </Card>
   );
};
