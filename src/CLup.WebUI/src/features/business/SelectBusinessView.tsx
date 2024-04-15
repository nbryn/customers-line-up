import React from 'react';
import {useNavigate} from 'react-router-dom';
import makeStyles from '@mui/styles/makeStyles';
import {Box, Chip} from '@mui/material';

import {useGetBusinessesByOwnerQuery} from './BusinessApi';
import {Header} from '../../shared/components/Texts';
import {BUSINESS_OVERVIEW_ROUTE} from '../../app/RouteConstants';
import {type BusinessTableRowData, BusinessTableWithMap} from './BusinessTableWithMap';

const useStyles = makeStyles(({
    box: {
        alignItems: 'center',
        display: 'flex',
        flexDirection: 'column',
    },
}));

export const SelectBusinessView: React.FC = () => {
    const {data: businesses} = useGetBusinessesByOwnerQuery();
    const navigate = useNavigate();
    const styles = useStyles();

    const action = {
        icon: () => <Chip size="small" label="Select" clickable color="primary" />,
        tooltip: 'Select Business',
        onClick: (_: any, rowData: BusinessTableRowData) => {
            navigate(`${BUSINESS_OVERVIEW_ROUTE}/${rowData.id!}`);
        },
    };

    return (
        <Box className={styles.box}>
            <Header text="Businesses" />
            <BusinessTableWithMap businesses={businesses ?? []} action={action} />
        </Box>
    );
};
