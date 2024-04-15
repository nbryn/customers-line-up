import React from 'react';
import {useNavigate} from 'react-router';
import Chip from '@mui/material/Chip';
import makeStyles from '@mui/styles/makeStyles';
import {Box} from '@mui/material';

import {useGetAllBusinessesQuery} from './BusinessApi';
import {CREATE_BOOKING_ROUTE} from '../../app/RouteConstants';
import {Header} from '../../shared/components/Texts';
import {type BusinessTableRowData, BusinessTableWithMap} from './BusinessTableWithMap';

const useStyles = makeStyles({
    box: {
        alignItems: 'center',
        display: 'flex',
        flexDirection: 'column',
    },
});

export const AllBusinessesView: React.FC = () => {
    const {data: businesses} = useGetAllBusinessesQuery();
    const navigate = useNavigate();
    const styles = useStyles();

    const action = {
        icon: () => <Chip size="small" label="Go to business" clickable color="primary" />,
        tooltip: 'See available time slots',
        onClick: (_: any, rowData: BusinessTableRowData) => {
            navigate(`${CREATE_BOOKING_ROUTE}/${rowData.id ?? ''}`);
        },
    };

    return (
        <Box className={styles.box}>
            <Header text="Available Businesses" />
            <BusinessTableWithMap businesses={businesses ?? []} action={action} />
        </Box>
    );
};
