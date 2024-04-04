import React, {type ReactNode} from 'react';
import Button from '@mui/material/Button';
import {Box} from '@mui/material';
import makeStyles from '@mui/styles/makeStyles';

import {Card} from './Card';

const useStyles = makeStyles({
    card: {
        textAlign: 'center',
    },
    children: {
        marginBottom: 15,
        marginTop: 15,
    },
    primaryButton: {
        width: '56%',
    },
    secondaryButton: {
        width: '54%',
    },
});

type Props = {
    buttonText: string;
    children?: ReactNode;
    primaryButtonDisabled?: boolean;
    secondaryButtonText?: string;
    subtitle?: string;
    title: string;
    secondaryAction?: () => void;
    buttonAction: () => void;
};

export const InfoCard: React.FC<Props> = ({
    buttonText,
    children,
    primaryButtonDisabled,
    secondaryButtonText,
    subtitle,
    title,
    secondaryAction,
    buttonAction,
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
            <Box marginBottom={4} marginTop={-4}>
                {children}
            </Box>
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
