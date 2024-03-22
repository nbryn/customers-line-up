import React from 'react';
import type {ReactElement} from 'react';
import CircularProgress from '@mui/material/CircularProgress';
import {Row} from 'react-bootstrap';
import makeStyles from '@mui/styles/makeStyles';

import {isLoading} from '../api/ApiState';
import {Table} from '../components/Table';
import type {TableColumn} from '../components/Table';

const useStyles = makeStyles(() => ({
    spinner: {
        justifyContent: 'center',
        marginTop: 100,
    },
}));

export type Props = {
    actions: any;
    columns: TableColumn[];
    tableData: any[];
    tableTitle: string | ReactElement;
    emptyMessage?: string;
};

export const TableContainer: React.FC<Props> = ({
    actions,
    columns,
    tableTitle,
    tableData,
    emptyMessage,
}: Props) => {
    const styles = useStyles();
    return (
        <>
            {isLoading ? (
                <Row className={styles.spinner}>
                    <CircularProgress />
                </Row>
            ) : (
                <Table
                    actions={actions}
                    columns={columns}
                    data={tableData?.map((item) => Object.assign({}, item))}
                    title={tableTitle}
                    emptyMessage={emptyMessage}
                />
            )}
        </>
    );
};
