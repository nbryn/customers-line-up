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
    tableTitle: string | ReactElement;
    loading: boolean;
    fetchTableData?: () => Promise<DTO[]> | any;
    tableData?: DTO[];
    removeEntryWithId?: string | null;
    emptyMessage?: string;
};

export const TableContainer: React.FC<Props> = ({
    actions,
    columns,
    tableTitle,
    loading,
    fetchTableData,
    removeEntryWithId,
    tableData,
    emptyMessage,
}: Props) => {
    const styles = useStyles();
    const [tableData2, setTableData] = useState<DTO[]>([]);

    useEffect(() => {
        (async () => {
            if (fetchTableData) {
                const tableData = await fetchTableData();

                setTableData(tableData);
            }
        })();
    }, []);

    useEffect(() => {
        const updatedData = tableData2.filter((b) => b.id !== removeEntryWithId);

        setTableData(updatedData);
    }, [removeEntryWithId]);

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
                    data={tableData?.map((item) => Object.assign({}, item)) ?? tableData2}
                    title={tableTitle}
                    emptyMessage={emptyMessage}
                />
            )}
        </>
    );
};
