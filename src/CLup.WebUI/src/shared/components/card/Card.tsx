import React from 'react';
import Button from '@mui/material/Button';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardHeader from '@mui/material/CardHeader';
import makeStyles from '@mui/styles/makeStyles';
import MUICard from '@mui/material/Card';

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
   buttonColor?: 'inherit' | 'primary' | 'secondary';
   buttonText?: string;
   buttonSize?: 'small' | 'medium' | 'large';
   disableButton?: boolean;
   variant?: 'outlined' | 'elevation';
   className?: string;
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
