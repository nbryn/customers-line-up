import React, {useState} from 'react';
import Button from 'react-bootstrap/Button';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Typography from '@material-ui/core/Typography';

type Props = {
    show: boolean;
    title: string;
    replyMode: boolean;
    text?: string;
    primaryDisabled?: boolean;
    subTitle?: string;
    close: () => void;
    onSubmit: () => void;
};

export const DialogModal: React.FC<Props> = ({
    show,
    title,
    replyMode,
    primaryDisabled,
    subTitle,
    text,
    close,
    onSubmit,
}: Props) => {

    return (
        <div>
            <Dialog open={show} onClose={close}>
                <DialogTitle>{title}</DialogTitle>
                <DialogContent>
                    <DialogContentText>{subTitle}</DialogContentText>
                    {!replyMode && <Typography>{text} </Typography>}
                    {replyMode && (
                        <TextField
                            autoFocus
                            margin="dense"
                            id="name"
                            label="Email Address"
                            type="email"
                            fullWidth
                            variant="standard"
                        />
                    )}
                </DialogContent>
                <DialogActions>
                    <Button variant="secondary" onClick={close}>
                        Close
                    </Button>
                    <Button variant="primary" disabled={primaryDisabled} onClick={onSubmit}>
                        {replyMode ? 'Send Message' : 'Reply'}
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
};
