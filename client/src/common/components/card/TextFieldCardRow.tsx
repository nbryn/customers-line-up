import React from 'react';
import Avatar from '@material-ui/core/Avatar';
import {Badge, Col, Row} from 'react-bootstrap';
import Chip from '@material-ui/core/Chip';
import DoneIcon from '@material-ui/icons/Done';
import {makeStyles} from '@material-ui/core/styles';
import Divider from '@material-ui/core/Divider';

import {TextField, TextFieldType} from '../form/TextField';
import TextFieldUtil from '../../util/TextFieldUtil';

const useStyles = makeStyles({
   root: {
      minWidth: 275,
   },
   row: {
      marginLeft: 0,
      marginTop: 25,
      marginBottom: 25,
   },
   chipCol: {
      textAlign: 'right',
      marginTop: 12,
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
   buttonText?: string;
   label: string | undefined;
   id: string;
   color?: 'primary' | 'secondary';
   variant?: 'outlined';
   type?: TextFieldType;
   value: string;
   buttonAction?: () => void;
};

export const TextFieldCardRow: React.FC<Props> = ({
   id: key,
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
                  id={key}
                  disabled={true}
                  label={label}
                  type={type}
                  value={value}
                  inputLabelProps={{
                     shrink: TextFieldUtil.shouldInputLabelShrink(key),
                  }}
               />
            </Col>
            {buttonAction && (
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
            )}
         </Row>
      </>
   );
};
