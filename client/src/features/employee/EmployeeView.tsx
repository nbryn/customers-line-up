import React, {useEffect} from 'react';
import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useLocation} from 'react-router-dom';
import {useSelector} from 'react-redux';

import {BusinessDTO} from '../business/Business';
import {deleteEmployee, fetchEmployeesByBusiness, selectEmployeesByBusiness} from './employeeSlice';
import {EmployeeDTO} from './Employee';
import {ErrorView} from '../../common/views/ErrorView';
import {Header} from '../../common/components/Texts';
import {TableColumn} from '../../common/components/Table';
import {TableContainer} from '../../common/containers/TableContainer';
import {isLoading, RootState, useAppDispatch, useAppSelector} from '../../app/Store';

const useStyles = makeStyles((theme) => ({
    row: {
        justifyContent: 'center',
    },
}));

interface LocationState {
    business: BusinessDTO;
}

export const EmployeeView: React.FC = () => {
    const styles = useStyles();
    const location = useLocation<LocationState>();
    const loading = useAppSelector(isLoading);

    const dispatch = useAppDispatch();

    if (!location.state) {
        return <ErrorView />;
    }
    const {business} = location.state;

    const employees = useSelector<RootState, EmployeeDTO[]>((state) =>
        selectEmployeesByBusiness(state, business.id)
    );

    useEffect(() => {
        dispatch(fetchEmployeesByBusiness(business.id));
    }, []);

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
                        loading={loading}
                        tableTitle="Employees"
                        tableData={employees}
                        emptyMessage="No Employees Yet"
                    />
                </Col>
            </Row>
        </>
    );
};
