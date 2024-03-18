import React, {useState} from 'react';
import Button from 'react-bootstrap/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import makeStyles from '@mui/styles/makeStyles';
import Typography from '@mui/material/Typography';

import {TextField} from '../form/TextField';

const useStyles = makeStyles(() => ({
    buttons: {
        marginTop: 25,
    },
    dialog: {
        marginTop: -290,
    },
    dialogReplyMode: {
        height: 175,
    },
    textField: {
        marginTop: -10,
    },
}));

type Props = {
    show: boolean;
    title: string;
    replyMode: boolean;
    text?: string;
    primaryDisabled?: boolean;
    subTitle?: string;
    close: () => void;
    onSubmit: (messageContent: string) => void;
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
    const styles = useStyles();
    const [newMessageContent, setNewMessageContent] = useState('');

    return (
        <>
            <Dialog open={show} onClose={close} className={styles.dialog}>
                <DialogTitle>{title}</DialogTitle>
                <DialogContent className={replyMode ? styles.dialogReplyMode : undefined} dividers>
                    <DialogContentText>{subTitle}</DialogContentText>
                    {!replyMode && <Typography>{text} </Typography>}
                    {replyMode && (
                        <TextField
                            className={styles.textField}
                            value={newMessageContent}
                            margin="dense"
                            id="name"
                            label="Message"
                            type="email"
                            variant="standard"
                            maxLength={100}
                            onChange={(e) => setNewMessageContent(e.target.value)}
                            autoFocus
                            multiline
                        />
                    )}
                </DialogContent>
                <DialogActions className={styles.buttons}>
                    <Button variant="secondary" onClick={close}>
                        Close
                    </Button>
                    <Button
                        variant="primary"
                        disabled={primaryDisabled || (replyMode && newMessageContent.length < 5)}
                        onClick={() => {
                            onSubmit(newMessageContent);
                            setNewMessageContent('');
                        }}
                    >
                        {replyMode ? 'Send Message' : 'Reply'}
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
};
