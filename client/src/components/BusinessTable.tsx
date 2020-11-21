import MaterialTable from 'material-table';
import React, {forwardRef} from 'react';

import {BusinessDTO} from '../services/dto/Business';

type TableColumn = {
   title: string;
   field: string;
};

export type BusinessData = {
   name: string;
   zip: string;
   opens: string;
   closes: string;
   type: string;
};

const columns: TableColumn[] = [
   {title: 'Name', field: 'name'},
   {title: 'Zip', field: 'zip'},
   {title: 'Opens', field: 'opens'},
   {title: 'Closes', field: 'closes'},
   {title: 'Type', field: 'type'},
];

type Props = {
   data: BusinessDTO[];
};


export const BusinessTable: React.FC<Props> = ({data}: Props) => {
   return (
      <MaterialTable columns={columns} data={data} title="Businesses" ></MaterialTable>
   );
};
