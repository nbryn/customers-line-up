import React from 'react';
import type {FormEvent} from 'react';
import {Alert} from 'react-bootstrap';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import CircularProgress from '@mui/material/CircularProgress';
import makeStyles from '@mui/styles/makeStyles';

import {isLoading, selectApiState} from '../../api/ApiState';
import {useAppSelector} from '../../../app/Store';

const useStyles = makeStyles(() => ({
    alert: {
        display: 'inline-block',
        marginTop: -20,
        marginBottom: 40,
        maxWidth: 380,
    },
    form: {
        marginTop: 8,
    },
    working: {
        transform: 'translateX(90%)',
        top: 50,
    },
}));

type Props = {
    children?: React.ReactNode;
    submitButtonText: string;
    valid: boolean;
    submitButtonStyle?: string;
    showMessage?: boolean;
    onSubmit: (event: FormEvent<HTMLFormElement>) => void;
};

export const Form: React.FC<Props> = ({
    children,
    submitButtonText,
    submitButtonStyle,
    valid,
    showMessage = false,
    onSubmit,
}) => {
    const styles = useStyles();
    const apiState = useAppSelector(selectApiState);
    const loading = useAppSelector(isLoading);

    return (
        <Box component="form" onSubmit={onSubmit} className={styles.form} noValidate>
            {showMessage && apiState.message && (
                <Alert className={styles.alert} variant="danger">
                    {apiState.message}
                </Alert>
            )}

            {loading ? (
                <CircularProgress className={styles.working} />
            ) : (
                <>
                    {children}
                    <Button
                        className={submitButtonStyle}
                        disabled={!valid}
                        type="submit"
                        variant="contained"
                        fullWidth
                    >
                        {submitButtonText}
                    </Button>
                </>
            )}
        </Box>
    );
};
