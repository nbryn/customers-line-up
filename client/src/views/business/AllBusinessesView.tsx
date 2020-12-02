import React from 'react';
import {useHistory} from 'react-router';

import {ALL_BUSINESSES_URL} from '../../api/URL';
import {BusinessDTO} from '../../models/dto/Business';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';


export const AllBusinessesView: React.FC = () => {
   const history = useHistory();
   const requestHandler: RequestHandler<BusinessDTO[]> = useRequest();

   const columns: TableColumn[] = [
      {title: 'id', field: 'id', hidden: true},
      {title: 'Name', field: 'name'},
      {title: 'Zip', field: 'zip'},
      {title: 'Opens', field: 'opens'},
      {title: 'Closes', field: 'closes'},
      {title: 'Type', field: 'type'},
   ];

   const actions = [
      {
         icon: 'info',
         tooltip: 'See available time slots',
         onClick: (event: any, rowData: BusinessDTO) => {
            history.push('/booking', {data: rowData});
         },
      },
   ];

   return (
      <TableContainer
         actions={actions}
         columns={columns}
         fetchTableData={async () => await requestHandler.query(ALL_BUSINESSES_URL)}
         badgeTitle="Available Businesses"
         tableTitle="Businesses"
      />
   );
};
