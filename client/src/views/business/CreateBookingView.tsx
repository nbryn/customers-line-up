import React from 'react';
import {useLocation, useHistory} from 'react-router-dom';

import {BusinessDTO, TimeSlotDTO} from '../../models/dto/Business';
import {Modal} from '../../components/Modal';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';
import URL from '../../api/URL';

interface LocationState {
   data: BusinessDTO;
}

const SUCCESS_MESSAGE = 'Booking Made - Go to my bookings to see your bookings';

export const CreateBookingView: React.FC = () => {
   const location = useLocation<LocationState>();
   const history = useHistory();

   const requestHandler: RequestHandler<TimeSlotDTO[]> = useRequest(SUCCESS_MESSAGE);

   const business: BusinessDTO = location.state.data;

   const columns: TableColumn[] = [
      {title: 'id', field: 'id', hidden: true},
      {title: 'Date', field: 'date'},
      {title: 'Start', field: 'start'},
      {title: 'End', field: 'end'},
   ];

   const actions = [
      {
         icon: 'book',
         tooltip: 'Book Time',
         onClick: async (event: any, rowData: TimeSlotDTO) => {
            console.log(rowData.id);

            requestHandler.mutation(URL.getCreateBookingURL(rowData.id), 'POST');
         },
      },
   ];
   return (
      <>
         <TableContainer
            actions={actions}
            columns={columns}
            fetchTableData={async () => await requestHandler.query(URL.getTimeSlotURL(business.id!))}
            badgeTitle={`Available Time Slots For ${business.name}`}
            tableTitle="Time Slots"
            emptyMessage="No Time Slots Available"
         />

         <Modal
            show={requestHandler.requestInfo ? true : false}
            title="Booking Info"
            text={requestHandler.requestInfo}
            secondaryAction={() => requestHandler.setRequestInfo('')}
            primaryAction={() => history.push('/mybookings')}
            primaryActionText="My Bookings"
         />
      </>
   );
};
