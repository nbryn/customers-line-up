import React from 'react';
import Button from '@material-ui/core/Button';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardHeader from '@material-ui/core/CardHeader';
import {makeStyles} from '@material-ui/core/styles';
import MUICard from '@material-ui/core/Card';

const useStyles = makeStyles({
   root: {
      minWidth: 275,
   },
   button: {
      justifyContent: 'center',
   },
   content: {
      padding: 5,
   },
   header: {
      textAlign: 'center',
      marginBottom: 15,
   },
});

type Props = {
   title?: string;
   subtitle?: string;
   buttonAction?: () => void;
   buttonColor?: 'inherit' | 'default' | 'primary' | 'secondary';
   buttonText?: string;
   buttonSize?: 'small' | 'medium' | 'large';
   disableButton?: boolean;
   variant?: 'outlined' | 'elevation';
   className?: any;
   buttonStyle?: any;
   children?: React.ReactNode;
};

export const Card: React.FC<Props> = ({
   children,
   className,
   buttonAction,
   buttonColor,
   buttonText,
   buttonSize,
   disableButton = false,
   title,
   subtitle,
   variant,
   buttonStyle,
}: Props) => {
   const styles = useStyles();

   return (
      <MUICard className={className} variant={variant}>
         <CardHeader className={styles.header} title={title} subheader={subtitle} />
         <CardContent className={styles.content}>{children}</CardContent>
         <CardActions className={styles.button}>
            {buttonText && (
               <Button
                  className={buttonStyle}
                  variant="contained"
                  color={buttonColor}
                  onClick={buttonAction}
                  size={buttonSize}
                  disabled={disableButton}
               >
                  {buttonText}
               </Button>
            )}
         </CardActions>
      </MUICard>
   );
};
