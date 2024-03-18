import React from 'react';
import {
    AssignmentTurnedIn,
    Check,
    CheckCircle,
    Edit,
    Grain,
    Home,
    LocationCity,
    Whatshot,
} from '@mui/icons-material';
import makeStyles from '@mui/styles/makeStyles';

const useStyles = makeStyles((theme) => ({
    icon: {
        marginRight: theme.spacing(0.5),
        width: 20,
        height: 20,
    },
}));

type Props = {
    icon: string;
};

export const Icons: React.FC<Props> = ({icon}: Props) => {
    const styles = useStyles();

    switch (icon) {
        case 'Check':
            return <Check className={styles.icon} />;
        case 'CheckCircle':
            return <CheckCircle className={styles.icon} />;
        case 'City':
            return <LocationCity className={styles.icon} />;
        case 'Edit':
            return <Edit className={styles.icon} />;
        case 'Home':
            return <Home className={styles.icon} />;
        case 'Hot':
            return <Whatshot className={styles.icon} />;
        case 'Grain':
            return <Grain className={styles.icon} />;
        case 'TurnedIn':
            return <AssignmentTurnedIn className={styles.icon} />;
        default:
            return <Home className={styles.icon} />;
    }
};
