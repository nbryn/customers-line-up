import React from 'react';
import type {ReactElement} from 'react';
import {Badge} from 'react-bootstrap';
import MaterialTable from 'material-table';

export type TableColumn = {
    title: string;
    field: string;
    hidden?: boolean;
};

export type BusinessData = {
    name: string;
    zip: string;
    opens: string;
    closes: string;
    type: string;
};

type Props = {
    actions: any;
    columns: TableColumn[];
    data: any;
    title: string | ReactElement;
    emptyMessage?: string;
};

export const Table: React.FC<Props> = ({actions, columns, data, title, emptyMessage}: Props) => {
    return (
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
    );
};
