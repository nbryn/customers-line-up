import React, {type ReactNode} from 'react';
import Button from '@mui/material/Button';
import MuiDialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';
import makeStyles from '@mui/styles/makeStyles';
import {Typography} from '@mui/material';

const useStyles = makeStyles({
    Dialog: {
        position: 'relative',
        marginLeft: 180,
        marginTop: -135,
    },
});

type Props = {
    open: boolean;
    title?: string;
    text?: string;
    primaryDisabled?: boolean;
    primaryActionText?: string;
    children?: ReactNode;
    primaryAction?: () => void;
    secondaryAction?: () => void;
};

export const Dialog: React.FC<Props> = ({
    open,
    title,
    text,
    primaryDisabled = false,
    primaryActionText,
    children,
    primaryAction,
    secondaryAction,
}: Props) => {
    const styles = useStyles();
    return (
        <MuiDialog className={styles.Dialog} onClose={secondaryAction} open={open}>
            <DialogTitle>
                <Typography variant="h5" align="center">
                    <b>{title}</b>
                </Typography>
            </DialogTitle>
            <IconButton
                aria-label="close"
                onClick={secondaryAction}
                sx={{
                    position: 'absolute',
                    right: 8,
                    top: 8,
                    color: (theme) => theme.palette.grey[500],
                }}
            >
                <CloseIcon />
            </IconButton>

            <DialogContent dividers>
                <p>{text}</p>
                {children}
            </DialogContent>

            <DialogActions>
                <Button autoFocus onClick={secondaryAction}>
                    {'Close'}
                </Button>
                {primaryAction && (
                    <Button autoFocus onClick={primaryAction} disabled={primaryDisabled}>
                        {primaryActionText}
                    </Button>
                )}
            </DialogActions>
        </MuiDialog>
    );
};
