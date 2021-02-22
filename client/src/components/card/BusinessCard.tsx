import React from 'react';
import Button from '@material-ui/core/Button';
import {makeStyles} from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';

import {BusinessDTO} from '../../models/Business';
import {Card} from './Card';

const useStyles = makeStyles({
    root: {
        minWidth: 275,
    },
    card: {
        textAlign: 'center',
        marginBottom: 15,
    },
    primaryButton: {
        width: '54%',
        marginTop: 25,
    },
    secondaryButton: {
        width: '54%',
        marginBottom: 15,
    },
});

type Props = {
    business: BusinessDTO;
    buttonAction: () => void;
    buttonText: string;
    secondaryAction?: () => void;
    secondaryButtonText?: string;
};

export const BusinessCard: React.FC<Props> = ({
    buttonAction,
    buttonText,
    business,
    secondaryAction,
    secondaryButtonText,
}: Props) => {
    const styles = useStyles();

    return (
        <Card
            className={styles.card}
            buttonAction={secondaryAction}
            buttonColor="secondary"
            buttonText={secondaryButtonText}
            buttonSize="small"
            title={business.name}
            subtitle={business.type}
            variant="outlined"
            buttonStyle={styles.secondaryButton}
        >
            <div className={styles.card}>
                <Typography>{business.zip} </Typography>
                <Typography>{business.address} </Typography>
            </div>
            <Button
                className={styles.primaryButton}
                variant="contained"
                color="primary"
                onClick={buttonAction}
                size="small"
            >
                {buttonText}
            </Button>
        </Card>
    );
};
