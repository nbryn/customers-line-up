import React from 'react';
import {Col, Row} from 'react-bootstrap';
import Divider from '@material-ui/core/Divider';
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
   divider: {
      marginLeft: 60,
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
         <Row>
            <Col sm={12} md={6} lg={data.length < 5 ? 12 : 5}>
               {data.slice(0, 4).map((x) => (
                  <TextFieldCardRow
                     key={x.key}
                     label={x.label}
                     type={x.type}
                     value={x.value}
                     buttonText={x.buttonText}
                     buttonAction={x.buttonAction}
                  />
               ))}
            </Col>
            <Divider className={styles.divider} orientation="vertical" flexItem />
            <Col className={styles.divider} sm={12} md={6} lg={5}>
               {data.slice(4).map((x) => (
                  <TextFieldCardRow
                     key={x.key}
                     label={x.label}
                     type={x.type}
                     value={x.value}
                     buttonText={x.buttonText}
                     buttonAction={x.buttonAction}
                  />
               ))}
            </Col>
         </Row>
         <div className={styles.end}></div>
      </Card>
   );
};
