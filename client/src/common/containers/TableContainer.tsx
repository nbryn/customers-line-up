import React, {ReactElement, useEffect, useState} from 'react';
import CircularProgress from '@material-ui/core/CircularProgress';
import {Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';

import {DTO} from '../models/General';
import {Table, TableColumn} from '../components/Table';

const useStyles = makeStyles((theme) => ({
    spinner: {
        justifyContent: 'center',
        marginTop: 100,
    },
}));

export type Props = {
    actions: any;
    columns: TableColumn[];
    loading: boolean;
    tableData: DTO[];
    tableTitle: string | ReactElement;
    emptyMessage?: string;
    fetchData: () => void;
};

export const TableContainer: React.FC<Props> = ({
    actions,
    columns,
    tableTitle,
    loading,
    tableData,
    emptyMessage,
    fetchData,
}: Props) => {
    const styles = useStyles();

    useEffect(() => {
        (async () => {
            fetchData();
        })();
    }, []);

    return (
        <>
            {loading ? (
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
