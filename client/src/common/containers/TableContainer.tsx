import React, {ReactElement, useEffect, useState} from 'react';
import CircularProgress from '@material-ui/core/CircularProgress';
import {Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';

import {DTO} from '../../app/General';
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
    fetchTableData: () => Promise<DTO[]>;
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
    emptyMessage,
}: Props) => {
    const styles = useStyles();
    const [tableData, setTableData] = useState<DTO[]>([]);

    useEffect(() => {
        (async () => {
            const tableData = await fetchTableData();

            setTableData(tableData);
        })();
    }, []);

    useEffect(() => {
        const updatedData = tableData.filter((b) => b.id !== removeEntryWithId);

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
                    data={tableData}
                    title={tableTitle}
                    emptyMessage={emptyMessage}
                />
            )}
        </>
    );
};
