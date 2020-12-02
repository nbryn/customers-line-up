import React from 'react';

import {BUSINESSES_OWNER_URL} from '../../api/URL';
import {BusinessDTO} from '../../models/dto/Business';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';

export const OwnerBusinessesView: React.FC = () => {
   const requestHandler: RequestHandler<BusinessDTO[]> = useRequest();

   const columns: TableColumn[] = [
      {title: 'Name', field: 'name'},
      {title: 'Zip', field: 'zip'},
      {title: 'Opens', field: 'opens'},
      {title: 'Closes', field: 'closes'},
      {title: 'Type', field: 'type'},
   ];

   const actions = [
      {
         icon: 'edit',
         tooltip: 'Edit Business',
         onClick: (event: any, rowData: any) => {
            console.log(rowData);
         },
      },
   ];

   return (
      <TableContainer
         actions={actions}
         columns={columns}
         fetchTableData={async () => await requestHandler.query(BUSINESSES_OWNER_URL)}
         badgeTitle="Your Businesses"
         tableTitle="Businesses"
      />
   );
};
