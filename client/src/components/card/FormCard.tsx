import React from 'react';
import {makeStyles} from '@material-ui/core/styles';

import {Card} from './Card';
import {TextFieldCardRow} from './TextFieldCardRow';
import {TextFieldType} from '../form/TextField';

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

export type FormCardData = {
    label: string | undefined;
    key: string;
    color?: 'primary' | 'secondary';
    variant?: 'outlined';
    type?: TextFieldType;
    value: string;
    buttonAction?: () => void;
    buttonText?: string;
 };

type Props = {
   data: FormCardData[];
   buttonAction?: () => void;
   title: string;
   buttonText?: string;
};

export const FormCard: React.FC<Props> = ({buttonAction, buttonText, title, data}: Props) => {
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
            <TextFieldCardRow
               key={x.key}          
               label={x.label}
               type={x.type}
               value={x.value}
               buttonText={x.buttonText}
               buttonAction={x.buttonAction}
            />
         ))}
         <div className={styles.end}></div>
      </Card>
   );
};
