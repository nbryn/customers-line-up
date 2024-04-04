import React from 'react';
import type {ReactElement} from 'react';
import {Badge} from 'react-bootstrap';
import MaterialTable from 'material-table';
import makeStyles from '@mui/styles/makeStyles';
import CircularProgress from '@mui/material/CircularProgress';
import {Grid} from '@mui/material';

import {useAppSelector} from '../../app/Store';
import {isLoading} from '../api/ApiState';

const useStyles = makeStyles({
    spinner: {
        justifyContent: 'center',
        marginTop: 100,
    },
});

export type TableColumn = {
    title: string;
    field: string;
    hidden?: boolean;
};

export type TableData = {
    [key: string]: string;
}[];

type Props = {
    actions: any;
    columns: TableColumn[];
    data: TableData;
    title: string | ReactElement;
    emptyMessage?: string;
};

export const Table: React.FC<Props> = ({actions, columns, data, title, emptyMessage}: Props) => {
    const loading = useAppSelector(isLoading);
    const styles = useStyles();

    return (
        <>
            {loading ? (
                <Grid item className={styles.spinner}>
                    <CircularProgress />
                </Grid>
            ) : (
                <MaterialTable
                    columns={columns}
                    data={data}
                    actions={actions}
                    title={title}
                    localization={{
                        body: {
                            emptyDataSourceMessage: (
                                <h4>
                                    <Badge style={{marginBottom: 50}} variant="primary">
                                        {emptyMessage}
                                    </Badge>
                                </h4>
                            ),
                        },
                    }}
                    options={{actionsColumnIndex: -1}}
                />
            )}
        </>
    );
};
