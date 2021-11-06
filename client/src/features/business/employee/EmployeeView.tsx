import React from 'react';
import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useSelector} from 'react-redux';

import {deleteEmployee, fetchEmployeesByBusiness, selectEmployeesByBusiness} from './employeeSlice';
import {EmployeeDTO} from './Employee';
import {ErrorView} from '../../../shared/views/ErrorView';
import {Header} from '../../../shared/components/Texts';
import {selectCurrentBusiness} from '../businessSlice';
import {RootState, useAppDispatch, useAppSelector} from '../../../app/Store';
import {TableColumn} from '../../../shared/components/Table';
import {TableContainer} from '../../../shared/containers/TableContainer';

const useStyles = makeStyles((theme) => ({
    row: {
        justifyContent: 'center',
    },
}));

export const EmployeeView: React.FC = () => {
    const styles = useStyles();
    const dispatch = useAppDispatch();
    const business = useAppSelector(selectCurrentBusiness);

    if (!business) {
        return <ErrorView />;
    }

    const employees = useSelector<RootState, EmployeeDTO[]>((state) =>
        selectEmployeesByBusiness(state, business.id)
    );

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
                dispatch(
                    deleteEmployee({
                        id: employee.businessId!,
                        data: employee.privateEmail!,
                    })
                );
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
                        fetchData={() => dispatch(fetchEmployeesByBusiness(business.id))}
                        tableTitle="Employees"
                        tableData={employees}
                        emptyMessage="No Employees Yet"
                    />
                </Col>
            </Row>
        </>
    );
};
