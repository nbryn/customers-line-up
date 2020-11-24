import {Badge, Col, Container} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import React, {useEffect, useState} from 'react';

import {BusinessQueueDTO} from '../../models/dto/Business';
import {Table, TableColumn} from '../../components/Table';

export const BookingView: React.FC = () => {
   const [timeSlots, setTimeSlots] = useState<BusinessQueueDTO[]>([]);

   const columns: TableColumn[] = [
      {title: 'Start', field: 'start'},
      {title: 'End', field: 'end'},
   ];

   const actions = [
      {
         icon: 'book',
         tooltip: 'Book Time',
         onClick: (event: any, rowData: any) => {
            console.log(rowData);
         },
      },
   ];
   return (
      <Container>
         <div
            style={{
               position: 'absolute',
               left: '35%',
               width: 2000,
               textAlign: 'center',
            }}
         >
            <Col sm={8} md={8} lg={4}>
               <h1>
                  <Badge style={{marginBottom: 50}} variant="primary">
                     Available Time Slots
                  </Badge>
               </h1>
               {timeSlots.length === 0 ? (
                  <CircularProgress />
               ) : (
                  <Table actions={actions} columns={columns} data={timeSlots} title="Time Slots" />
               )}
            </Col>
         </div>
      </Container>
   );
};
