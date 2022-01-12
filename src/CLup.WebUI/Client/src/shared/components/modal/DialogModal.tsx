import React, {useState} from 'react';
import Button from 'react-bootstrap/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import {makeStyles} from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';

import {TextField} from '../form/TextField';

const useStyles = makeStyles((theme) => ({
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
                        disabled={primaryDisabled || replyMode && newMessageContent.length < 5}
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
