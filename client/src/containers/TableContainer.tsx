import {Badge, Col, Container, Row} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';

import {DTO} from '../models/dto/Business';
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
   badgeTitle: string;
   tableTitle: string;
   fetchTableData: () => Promise<DTO[]>;
   removeEntryId?: number | null;
   emptyMessage?: string;
};

export const TableContainer: React.FC<Props> = ({
   actions,
   badgeTitle,
   columns,
   tableTitle,
   fetchTableData,
   removeEntryId,
   emptyMessage,
}: Props) => {
   const styles = useStyles();
   
   const [loading, setLoading] = useState<boolean>(true);
   const [tableData, setTableData] = useState<DTO[]>([]);

   useEffect(() => {
      (async () => {
         const tableData = await fetchTableData();

         setTableData(tableData);
         setLoading(false);
      })();
   }, []);

   useEffect(() => {
      const updatedData = tableData.filter((b) => b.id !== removeEntryId);
    
      setTableData(updatedData);
   }, [removeEntryId]);

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
               {loading ? (
                  <CircularProgress />
               ) : (
                  <Table
                     actions={actions}
                     columns={columns}
                     data={tableData}
                     title={tableTitle}
                     emptyMessage={emptyMessage}
                  />
               )}
            </Col>
         </Row>
      </Container>
   );
};
