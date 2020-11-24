import MaterialTable from 'material-table';
import IRowData from 'material-table/types';
import React from 'react';

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
   title: string;
};

export const Table: React.FC<Props> = ({actions, columns, data, title}: Props) => {
   return (
      <MaterialTable
         columns={columns}
         data={data}
         actions={actions}
         title={title}
      ></MaterialTable>
   );
};
