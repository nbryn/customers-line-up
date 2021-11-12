import Avatar from '@material-ui/core/Avatar';
import {Badge, Col, Row} from 'react-bootstrap';
import Chip from '@material-ui/core/Chip';
import DoneIcon from '@material-ui/icons/Done';
import React from 'react';
import {makeStyles} from '@material-ui/core/styles';
import Divider from '@material-ui/core/Divider';

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
   text: string;
   data: string | number | undefined;
   buttonText: string;
   color?: 'primary' | 'secondary';
   variant?: 'outlined';
   buttonAction: () => void;
};

export const CardRow: React.FC<Props> = ({
   text,
   data,
   buttonText,
   color,
   variant,
   buttonAction,
}: Props) => {
   const styles = useStyles();

   return (
      <>
         <Divider className={styles.divider} />
         <Row className={styles.row}>
            <Col>
               <Badge>
                  <h6>{text}</h6>
               </Badge>
            </Col>
            <Col className={styles.dataCol}>
               <Badge>
                  <h6>{data}</h6>
               </Badge>
            </Col>
            <Col className={styles.chipCol}>
               <Chip
                  className={styles.chip}
                  size="small"
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
