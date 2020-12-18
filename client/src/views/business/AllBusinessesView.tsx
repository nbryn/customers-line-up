import {Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React from 'react';
import {useHistory} from 'react-router';

import {ALL_BUSINESSES_URL} from '../../api/URL';
import {BusinessDTO} from '../../dto/Business';
import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';

const useStyles = makeStyles((theme) => ({
   row: {
      justifyContent: 'center',
   },
}));

export const AllBusinessesView: React.FC = () => {
   const styles = useStyles();
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
            history.push('/new/booking', {data: rowData});
         },
      },
   ];

   return (
      <Container>
         <Row className={styles.row}>
            <Header text="Available Businesses" />
         </Row>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={10}>
               <TableContainer
                  actions={actions}
                  columns={columns}
                  fetchTableData={async () => await requestHandler.query(ALL_BUSINESSES_URL)}
                  tableTitle="Businesses"
               />
            </Col>
         </Row>
      </Container>
   );
};
