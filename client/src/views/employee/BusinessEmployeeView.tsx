import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';
import {useLocation} from 'react-router-dom';

import URL from '../../api/URL';
import {EmployeeDTO} from '../../models/Employee';
import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../hooks/useRequest';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';
import {BusinessDTO} from '../../models/Business';

const useStyles = makeStyles((theme) => ({
   row: {
      justifyContent: 'center',
   },
}));

interface LocationState {
   business: BusinessDTO;
}

export const BusinessEmployeeView: React.FC = () => {
   const styles = useStyles();
   const location = useLocation<LocationState>();
   const [removeEmployee, setRemoveEmployee] = useState<number | null>(null);

   const requestHandler: RequestHandler<EmployeeDTO[]> = useRequest();

   const {business} = location.state;

   const columns: TableColumn[] = [
      {title: 'BusinessId', field: 'businessId', hidden: true},
      {title: 'Name', field: 'name'},
      {title: 'Employed Since', field: 'employedSince'},
      {title: 'Private Email', field: 'privateEmail'},
      {title: 'Company Email', field: 'companyEmail'},
   ];

   const actions = [
      {
         icon: () => <Chip size="small" label="Remove Employee" clickable color="primary" />,
         onClick: async (event: any, employee: EmployeeDTO) => {
            await requestHandler.mutation(URL.getDeleteEmployeeURL(employee.privateEmail!, employee.businessId!), 'DELETE');

            setRemoveEmployee(employee.id);
         },
      },
   ];

   return (
      <>
         <Row className={styles.row}>
            <Header text={business.name} />
         </Row>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={10}>
               <TableContainer
                  actions={actions}
                  columns={columns}
                  fetchTableData={async () =>
                     await requestHandler.query(URL.getEmployeesURL(business.id))
                  }
                  tableTitle="Employees"
                  emptyMessage="No Employees Yet"
                  removeEntryId={removeEmployee}
               />
            </Col>
         </Row>
      </>
   );
};
