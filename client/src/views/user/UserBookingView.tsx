import React, {useEffect, useState} from 'react';

import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {TimeSlotDTO} from '../../models/dto/Business';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';
import URL, {USER_BOOKINGS_URL} from '../../api/URL';

export const UserBookingView: React.FC = () => {
   const [bookings, setbookings] = useState<TimeSlotDTO[]>([]);

   const requestHandler: RequestHandler<TimeSlotDTO[]> = useRequest();

   useEffect(() => {
      (async () => {
         const bookings: TimeSlotDTO[] = await requestHandler.query(USER_BOOKINGS_URL);

         setbookings(bookings);
      })();
   }, []);

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
            const _bookings = bookings.filter((b) => b.id !== rowData.id);
            setbookings(_bookings);

            await requestHandler.mutation(URL.getDeleteBookingURL(rowData.id), 'DELETE');
         },
      },
   ];
   return (
      <TableContainer
         actions={actions}
         columns={columns}
         data={bookings}
         loading={requestHandler.working}
         badgeTitle="Your Bookings"
         tableTitle="Bookings"
         emptyMessage="No Bookings Yet"
      />
   );
};
