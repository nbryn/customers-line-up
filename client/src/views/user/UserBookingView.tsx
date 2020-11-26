import {Badge, Col, Container} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import React, {useEffect, useState} from 'react';

import {BusinessQueueDTO} from '../../models/dto/Business';
import UserService from '../../services/UserService';
import {Table, TableColumn} from '../../components/Table';


export const UserBookingView: React.FC = () => {
    const [queues, setQueues] = useState<BusinessQueueDTO[]>([]);

    useEffect(() => {
       (async () => {
          const queues: BusinessQueueDTO[] = await UserService.fetchUserBookings();
 
          setQueues(queues);
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
          onClick: async (event: any, rowData: BusinessQueueDTO) => {
             await UserService.removeBooking(rowData.id);
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
                      Your Bookings
                   </Badge>
                </h1>
                {queues.length === 0 ? (
                   <CircularProgress />
                ) : (
                   <Table actions={actions} columns={columns} data={queues} title="Bookings" />
                )}
             </Col>
          </div>
       </Container>
    );
};
