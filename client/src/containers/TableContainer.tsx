import {Badge, Col, Container, Row} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import React from 'react';

import {Table, TableColumn} from '../components/Table';

const useStyles = makeStyles((theme) => ({
   badge: {
      marginTop: 15,
      marginBottom: 25,
      textAlign: 'center',
   },
   row: {
      justifyContent: 'center',
   },
}));

export type Props = {
   actions: any;
   columns: TableColumn[];
   data: Array<any>;
   loading: boolean;
   badgeTitle: string;
  
   tableTitle: string;
   emptyMessage?: string;
};

export const TableContainer: React.FC<Props> = ({
   actions,
   badgeTitle,
   columns,
   data,
   loading,
   tableTitle,
   emptyMessage,
}: Props) => {
   const styles = useStyles();

   return (
      <Container>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={9}>
               <h1 className={styles.badge}>
                  <Badge variant="primary">{badgeTitle}</Badge>
               </h1>
            </Col>
         </Row>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={10}>
               {loading? (
                  <CircularProgress />
               ) : (
                  <Table
                     actions={actions}
                     columns={columns}
                     data={data}
                     title={tableTitle}
                     emptyMessage={emptyMessage}
                  />
               )}
            </Col>
         </Row>
      </Container>
   );
};
