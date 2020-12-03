import React from 'react';
import {makeStyles} from '@material-ui/core/styles';
import MUICard from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardHeader from '@material-ui/core/CardHeader';
import Button from '@material-ui/core/Button';

const useStyles = makeStyles({
   root: {
      minWidth: 275,
   },
   button: {
      justifyContent: 'center',
      marginTop: 5,
   },
});

type Props = {
   title?: string;
   subTitle?: string;
   buttonAction?: () => void;
   buttonColor?: 'inherit' | 'default' | 'primary' | 'secondary';
   buttonText?: string;
   buttonSize?: 'small' | 'medium' | 'large';
   variant?: 'outlined' | 'elevation';
   className?: any;
   children: React.ReactNode;
};

export const Card: React.FC<Props> = ({
   children,
   className,
   buttonAction,
   buttonColor,
   buttonText,
   buttonSize,
   title,
   subTitle,
   variant,
}: Props) => {
   const styles = useStyles();

   return (
      <MUICard className={className} variant={variant}>
         <CardHeader title={title} subheader={subTitle} />
         <CardContent>{children}</CardContent>
         <CardActions className={styles.button}>
            {buttonText && (
               <Button
                  variant="contained"
                  color={buttonColor}
                  onClick={buttonAction}
                  size={buttonSize}
               >
                  {buttonText}
               </Button>
            )}
         </CardActions>
      </MUICard>
   );
};
