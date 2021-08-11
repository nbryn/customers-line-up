import React from 'react';
import Button from 'react-bootstrap/Button';
import {Row} from 'react-bootstrap';
import {makeStyles, Theme} from '@material-ui/core/styles';
import MuiAlert, {AlertProps, Color} from '@material-ui/lab/Alert';
import Snackbar from '@material-ui/core/Snackbar';

const Alert = (props: AlertProps) => {
    return <MuiAlert elevation={6} variant="filled" {...props} />;
};

const useStyles = makeStyles((theme: Theme) => ({
    alert: {
        '& .MuiButtonBase-root': {
            left: 5,
            top: 5,
        },
        '& .MuiAlert-action': {
            height: 5,
        },
    },
    alertText: {
        marginLeft: -5,
    },
    buttonRow: {
        marginBottom: -5,
        marginLeft: 220,
        marginRight: -25,
        marginTop: 15,
    },
    primaryButton: {
        marginLeft: 5,
    },
    root: {
        'width': '100%',
        '& > * + *': {
            marginTop: theme.spacing(2),
            marginLeft: 100,
        },
    },
}));

interface ToastProps {
    message: string;
    severity: Color;
    onClose: () => void;
}

export const ToastMessage = ({message, severity, onClose}: ToastProps) => {
    const styles = useStyles();

    return (
        <div className={styles.root}>
            <Snackbar
                anchorOrigin={{vertical: 'top', horizontal: 'center'}}
                open={message ? true : false}
                autoHideDuration={6000}
                onClose={onClose}
            >
                <Alert onClose={onClose} severity={severity}>
                    {message}
                </Alert>
            </Snackbar>
        </div>
    );
};

export interface ExtendedToastProps extends Omit<ToastProps, 'severity'> {
    primaryButtonText: string;
    primaryAction: () => void;
}

export const ExtendedToastMessage = ({
    message,
    primaryButtonText,
    onClose,
    primaryAction,
}: ExtendedToastProps) => {
    const styles = useStyles();

    return (
        <div className={styles.root}>
            <Snackbar
                anchorOrigin={{vertical: 'top', horizontal: 'center'}}
                open={message ? true : false}
                autoHideDuration={10000}
                onClose={onClose}
            >
                <Alert className={styles.alert} onClose={onClose} severity="success">
                    <Row className={styles.alertText}>{message}</Row>
                    <Row className={styles.buttonRow}>
                        <Button size="sm" variant="secondary" onClick={onClose}>
                            Close
                        </Button>
                        <Button
                            className={styles.primaryButton}
                            size="sm"
                            variant="primary"
                            onClick={primaryAction}
                        >
                            {primaryButtonText}
                        </Button>
                    </Row>
                </Alert>
            </Snackbar>
        </div>
    );
};
