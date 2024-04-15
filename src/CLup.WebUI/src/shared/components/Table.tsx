import React from 'react';
import type {ReactElement} from 'react';
import {Badge} from 'react-bootstrap';
import MaterialTable, {type Action} from 'material-table';
import CircularProgress from '@mui/material/CircularProgress';
import {Grid} from '@mui/material';

import {useAppSelector} from '../../app/Store';
import {isLoading} from '../api/ApiState';

export type TableData = {
    [key: string]: string;
}[];

export type TableColumn = {
    title: string;
    field: string;
    hidden?: boolean;
};

export type TableAction = {
    tooltip?: string;
    disabled?: boolean;
    position?: 'auto' | 'toolbar' | 'toolbarOnSelect' | 'row';
    icon: () => ReactElement;
    onClick: (event: any, rowData: any) => void;
};

type Props = {
    actions: TableAction[];
    columns: TableColumn[];
    data: TableData;
    title: string | ReactElement;
    noDataMessage?: string;
};

export const Table: React.FC<Props> = ({actions, columns, data, title, noDataMessage}: Props) => {
    const loading = useAppSelector(isLoading);
    return (
        <>
            {loading ? (
                <Grid item xs={12} display="flex" justifyContent="center" marginTop={15}>
                    <CircularProgress />
                </Grid>
            ) : (
                <MaterialTable
                    columns={columns}
                    data={data}
                    actions={actions as Action<any>[]}
                    title={title}
                    localization={{
                        body: {
                            emptyDataSourceMessage: (
                                <h4>
                                    <Badge style={{marginBottom: 50}} variant="primary">
                                        {noDataMessage ?? 'No data to display'}
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
