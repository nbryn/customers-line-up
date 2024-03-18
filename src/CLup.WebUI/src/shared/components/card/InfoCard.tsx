import React from 'react';
import type {ReactNode} from 'react';
import Button from '@mui/material/Button';
import makeStyles from '@mui/styles/makeStyles';

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
    buttonAction: () => void;
    buttonText: string;
    children?: ReactNode;
    primaryButtonDisabled?: boolean;
    secondaryAction?: () => void;
    secondaryButtonText?: string;
    subtitle?: string;
    title: string;
};

export const InfoCard: React.FC<Props> = ({
    buttonAction,
    buttonText,
    children,
    primaryButtonDisabled,
    secondaryAction,
    secondaryButtonText,
    subtitle,
    title,
}: Props) => {
    const styles = useStyles();

    return (
        <Card
            className={styles.card}
            buttonAction={secondaryAction}
            buttonColor="secondary"
            buttonText={secondaryButtonText}
            buttonSize="small"
            title={title}
            subtitle={subtitle}
            variant="outlined"
            buttonStyle={styles.secondaryButton}
        >
            <div className={styles.card}>{children}</div>
            <Button
                className={styles.primaryButton}
                disabled={primaryButtonDisabled}
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
