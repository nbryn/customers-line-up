import {Badge} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React from 'react';

const useStyles = makeStyles((theme) => ({
   header: {
      marginTop: 85,
      marginBottom: 35,
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
