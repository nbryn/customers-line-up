import React, {useEffect, useState} from 'react';
import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';

import {DialogModal} from '../../shared/components/modal/DialogModal';
import {Header} from '../../shared/components/Texts';
import {MessageDTO, MessageResponse, SendMessage} from '../models/General';
import StringUtil from '../util/StringUtil';
import {TableColumn} from '../../shared/components/Table';
import {TableContainer} from '../../shared/containers/TableContainer';

const useStyles = makeStyles((theme) => ({
    title: {
        marginTop: 10,
    },
    badge: {
        top: 0,
        marginLeft: 5,
    },
    row: {
        justifyContent: 'center',
    },
}));

type Props = {
    messageResponse: MessageResponse | null;
    sendMessage: (message: SendMessage) => void;
    fetchData: () => void;
};

const getMessages = (sent: boolean, messageResponse: MessageResponse | null) => {
    if (!messageResponse) return [];
    if (sent) return messageResponse.sentMessages;

    return messageResponse.receivedMessages;
};

const getTitle = (sent: boolean, tableColumn = true, capitalize = true) => {
    if (tableColumn) {
        const result = sent ? 'receiver' : 'sender';

        return capitalize ? StringUtil.capitalize(result) : result;
    }

    return sent ? 'Sent Messages' : 'Received Messages';
};

export const MessageContainer: React.FC<Props> = ({
    messageResponse,
    fetchData,
    sendMessage,
}: Props) => {
    const styles = useStyles();
    const [showDialog, setShowDialog] = useState(false);
    const [replyMode, setReplyMode] = useState(false);

    const [selectedMessage, setSelectedMessage] = useState<MessageDTO>();
    const [showSentMessages, setShowSentMessages] = useState(false);

    const handleSubmit = (newMessageContent: string) => {
        if (replyMode && selectedMessage) {
            const message: SendMessage = {
                receiverId: selectedMessage.senderId,
                senderId: selectedMessage.receiverId,
                title: 'Enquiry',
                content: newMessageContent,
                type: 'Enquiry',
            };

            sendMessage(message);
            setShowDialog(false);
        }

        setReplyMode(!replyMode);
    };

    useEffect(() => {
        setReplyMode(false);
    }, [showDialog]);

    const columns: TableColumn[] = [
        {title: 'id', field: 'id', hidden: true},
        {title: 'Title', field: 'title'},
        {title: getTitle(showSentMessages), field: getTitle(showSentMessages, true, false)},
        {title: 'Date', field: 'date'},
    ];

    const actions = [
        {
            icon: () => <Chip size="small" label="Open" clickable color="primary" />,
            tooltip: 'Open Message',
            onClick: (event: any, message: MessageDTO) => {
                setSelectedMessage(message);
                setShowDialog(true);
            },
        },
        {
            icon: () => <Chip size="small" label="Delete" clickable color="secondary" />,
            tooltip: 'Delete Message',
            onClick: (event: any, message: MessageDTO) => {
                console.log('Delete');
            },
        },
    ];

    return (
        <>
            <Row className={styles.row}>
                <Header text={getTitle(showSentMessages, false)} />
            </Row>

            <Row className={styles.row}>
                <Col sm={6} md={8} xl={12}>
                    <DialogModal
                        show={showDialog}
                        replyMode={replyMode}
                        title={replyMode ? 'Send Message' : selectedMessage?.title ?? ''}
                        text={selectedMessage?.content}
                        onSubmit={handleSubmit}
                        close={() => setShowDialog(false)}
                    />
                    <TableContainer
                        actions={actions}
                        columns={columns}
                        tableData={getMessages(showSentMessages, messageResponse)}
                        fetchData={fetchData}
                        emptyMessage="No messages yet"
                        tableTitle={
                            <>
                                <Chip
                                    className={styles.badge}
                                    size="small"
                                    label={`Show ${getTitle(!showSentMessages, false)}`}
                                    clickable
                                    color="secondary"
                                    onClick={() => setShowSentMessages(!showSentMessages)}
                                />
                            </>
                        }
                    />
                </Col>
            </Row>
        </>
    );
};
