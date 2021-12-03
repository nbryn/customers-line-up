import React, {useState} from 'react';
import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';

import {Header} from '../../shared/components/Texts';
import {MessageDTO} from '../models/General';
import {MessagesResponse} from '../../features/user/User';
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
    messageResponse: MessagesResponse | null;
    fetchData: () => void;
};

const getMessages = (sent: boolean, messageResponse: MessagesResponse | null) => {
    if (!messageResponse) return [];
    if (sent) return messageResponse.sentMessages;

    return messageResponse.receivedMessages;
};

const getTitle = (sent: boolean, tableColumn = true, capitalize = true) => {
    if (tableColumn) {
        const result = sent ? "receiver" : 'sender';

        return capitalize ? StringUtil.capitalize(result) : result;
    }

    return sent ? 'Sent Messages' : 'Received Messages';
};

export const MessageContainer: React.FC<Props> = ({messageResponse, fetchData}: Props) => {
    const styles = useStyles();
    const [showSentMessages, setShowSentMessages] = useState(false);

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
                console.log('Open');
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
