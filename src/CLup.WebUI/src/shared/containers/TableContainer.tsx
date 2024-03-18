import React, {useEffect} from 'react';
import type {ReactElement} from 'react';
import CircularProgress from '@mui/material/CircularProgress';
import {Row} from 'react-bootstrap';
import makeStyles from '@mui/styles/makeStyles';

import type {DTO} from '../models/General';
import {selectApiState} from '../api/ApiState';
import {Table} from '../components/Table';
import type {TableColumn} from '../components/Table';
import {useAppSelector} from '../../app/Store';

const useStyles = makeStyles(() => ({
    spinner: {
        justifyContent: 'center',
        marginTop: 100,
    },
}));

export type Props = {
    actions: any;
    columns: TableColumn[];
    tableData: DTO[];
    tableTitle: string | ReactElement;
    emptyMessage?: string;
    fetchData?: () => void;
};

export const TableContainer: React.FC<Props> = ({
    actions,
    columns,
    tableTitle,
    tableData,
    emptyMessage,
    fetchData,
}: Props) => {
    const styles = useStyles();
    const apiState = useAppSelector(selectApiState);

    useEffect(() => {
        (async () => {
            fetchData?.();
        })();
    }, []);

    return (
        <>
            {apiState.loading ? (
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
