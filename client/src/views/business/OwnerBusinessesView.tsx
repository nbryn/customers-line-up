import React, {useEffect, useState} from 'react';

import {BUSINESSES_OWNER_URL} from '../../api/URL';
import {BusinessDTO} from '../../models/dto/Business';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';


export const OwnerBusinessesView: React.FC = () => {
   const [businesses, setBusinesses] = useState<BusinessDTO[]>([]);

   const requestHandler: RequestHandler<BusinessDTO[]> = useRequest();

   useEffect(() => {
      (async () => {
         const businesses: BusinessDTO[] = await requestHandler.query(BUSINESSES_OWNER_URL);

         setBusinesses(businesses);
      })();
   }, []);

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
         data={businesses}
         loading={requestHandler.working}
         badgeTitle="Your Businesses"
         tableTitle="Businesses"
      />
   );
};
