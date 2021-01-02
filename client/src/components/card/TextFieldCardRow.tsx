import React, {ReactText} from 'react';
import Avatar from '@material-ui/core/Avatar';
import {Badge, Col, Row} from 'react-bootstrap';
import Chip from '@material-ui/core/Chip';
import DoneIcon from '@material-ui/icons/Done';
import {makeStyles} from '@material-ui/core/styles';
import Divider from '@material-ui/core/Divider';

import {FormHandler} from '../../hooks/useForm';
import {TextField, TextFieldType} from '../form/TextField';
import {UserDTO} from '../../models/User';

const useStyles = makeStyles({
   root: {
      minWidth: 275,
   },
   row: {
      marginTop: 25,
      marginBottom: 25,
   },
   chipCol: {
      textAlign: 'right',
   },
   chip: {
      width: '60%',
   },
   dataCol: {
      textAlign: 'center',
   },
   divider: {
      marginLeft: -5,
      marginRight: -5,
   },
});

type Props = {
   buttonText: string;
   label: string | undefined;
   id: string;
   color?: 'primary' | 'secondary';
   variant?: 'outlined';
   type?: TextFieldType;
   value: string;
   buttonAction: () => void;
};

export const CardRow: React.FC<Props> = ({
   id,
   label,
   type,
   buttonText,
   color,
   variant,
   value,
   buttonAction,
}: Props) => {
   const styles = useStyles();

   return (
      <>
         <Divider className={styles.divider} />
         <Row className={styles.row}>
            <Col>
               <TextField
                  //className={styles.textField}
                  id={id}
                  disabled={true}
                  label={label}
                  type={type}
                  value={formHandler.values[id] as string}
               />
            </Col>

            <Col className={styles.chipCol}>
               <Chip
                  className={styles.chip}
                  size="medium"
                  avatar={<Avatar>M</Avatar>}
                  label={buttonText}
                  clickable
                  color={color || 'primary'}
                  onClick={() => buttonAction()}
                  deleteIcon={<DoneIcon />}
                  variant={variant || 'default'}
               />
            </Col>
         </Row>
      </>
   );
};
