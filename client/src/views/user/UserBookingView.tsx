import React, {useState} from 'react';

import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {TimeSlotDTO} from '../../models/dto/Business';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';
import URL, {USER_BOOKINGS_URL} from '../../api/URL';

export const UserBookingView: React.FC = () => {
   const [idToBeRemoved, setIdToBeRemoved] = useState<number | null>(null);

   const requestHandler: RequestHandler<TimeSlotDTO[]> = useRequest();

   const columns: TableColumn[] = [
      {title: 'id', field: 'id', hidden: true},
      {title: 'Business', field: 'business'},
      {title: 'Date', field: 'date'},
      {title: 'Start', field: 'start'},
      {title: 'End', field: 'end'},
   ];

   const actions = [
      {
         icon: 'delete',
         tooltip: 'Delete Booking',
         onClick: async (event: any, rowData: TimeSlotDTO) => {
            setIdToBeRemoved(rowData.id);

            await requestHandler.mutation(URL.getDeleteBookingURL(rowData.id), 'DELETE');
         },
      },
   ];
   return (
      <TableContainer
         actions={actions}
         columns={columns}
         fetchTableData={async () => await requestHandler.query(USER_BOOKINGS_URL)}
         removeEntryId={idToBeRemoved}
         badgeTitle="Your Bookings"
         tableTitle="Bookings"
         emptyMessage="No Bookings Yet"
      />
   );
};
