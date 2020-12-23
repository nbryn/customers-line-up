import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React from 'react';
import {useHistory} from 'react-router';

import {ALL_BUSINESSES_URL} from '../../api/URL';
import {BusinessDTO} from '../../models/Business';
import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../hooks/useRequest';
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
      {title: 'Business Hours', field: 'businessHours'},
      {title: 'Type', field: 'type'},
   ];

   const actions = [
      {
         icon: () => <Chip size="small" label="Go to business" clickable color="primary" />,
         tooltip: 'See available time slots',
         onClick: (event: any, business: BusinessDTO) => {
            history.push('/booking/new', {business: business});
         },
      },
   ];

   return (
      <>
         <Row className={styles.row}>
            <Header text="Available Businesses" />
         </Row>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={10}>
               <TableContainer
                  actions={actions}
                  columns={columns}
                  fetchTableData={async () => {
                     const businesses = await requestHandler.query(ALL_BUSINESSES_URL);

                     return businesses.map((business) => ({
                        ...business,
                        businessHours: business.opens + ' - ' + business.closes,
                     }));
                  }}
                  tableTitle="Businesses"
               />
            </Col>
         </Row>
      </>
   );
};
