import {Badge} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React from 'react';

const useStyles = makeStyles((theme) => ({
   header: {
      marginTop: 15,
      marginBottom: 25,
      textAlign: 'center',
   },
}));

export type TextProps = {
   text: string;
};

export const Header: React.FC<TextProps> = ({text}: TextProps) => {
   const styles = useStyles();

   return (
      <h1 className={styles.header}>
         <Badge variant="primary">{text}</Badge>
      </h1>
   );
};
