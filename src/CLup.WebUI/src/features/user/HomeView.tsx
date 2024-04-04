import React from 'react';
import Grid from '@mui/material/Grid';
import {makeStyles} from '@mui/styles';
import {useNavigate} from 'react-router-dom';

import {CardInfo} from '../../shared/components/card/CardInfo';
import {InfoCard} from '../../shared/components/card/InfoCard';
import {useGetUserQuery} from './UserApi';
import {useGetBusinessesByOwnerQuery} from '../business/BusinessApi';
import {
    BUSINESS_ROUTE,
    CREATE_BUSINESS_ROUTE,
    USER_BOOKINGS_ROUTE,
    USER_BUSINESS_ROUTE,
} from '../../app/RouteConstants';
import {Box} from '@mui/material';
import {Header} from '../../shared/components/Texts';

const useStyles = makeStyles(() => ({
    box: {
        alignItems: 'center',
        display: 'flex',
        flexDirection: 'column',
    },
    card: {
        marginTop: -10,
    },
    center: {
        justifyContent: 'center',
    },
}));

export const HomeView: React.FC = () => {
    const navigate = useNavigate();
    const styles = useStyles();

    const {data: getBusinessesByOwnerResponse} = useGetBusinessesByOwnerQuery();
    const {data: getUserResponse} = useGetUserQuery();
    return (
        <Box className={styles.box}>
            <Header text={`Welcome ${getUserResponse?.user?.name}`} />
            <Grid container justifyContent="center" spacing={2}>
                <Grid item xs={4}>
                    <InfoCard
                        buttonAction={() => navigate(USER_BOOKINGS_ROUTE)}
                        buttonText="My Bookings"
                        secondaryAction={() => navigate(USER_BUSINESS_ROUTE)}
                        secondaryButtonText="New Booking"
                        title="Booking Info"
                        subtitle="Summary"
                    >
                        <CardInfo
                            infoTexts={[
                                {
                                    text: `Bookings: ${
                                        getUserResponse?.user?.bookings?.length ?? 0
                                    }`,
                                    icon: 'Home',
                                },
                                {
                                    text: `Next: ${
                                        getUserResponse?.user?.bookings?.[0]?.date ??
                                        'No upcoming bookings'
                                    }`,
                                    icon: 'Hot',
                                },
                                {
                                    text: `Where: ${
                                        getUserResponse?.user?.bookings?.[0]?.business ??
                                        'No upcoming bookings'
                                    }`,
                                    icon: 'Grain',
                                },
                            ]}
                        />
                    </InfoCard>
                </Grid>
                <Grid item xs={4}>
                    <InfoCard
                        buttonAction={() => navigate(BUSINESS_ROUTE)}
                        buttonText="My Businesses"
                        primaryButtonDisabled={!getBusinessesByOwnerResponse?.businesses?.length}
                        secondaryAction={() => navigate(CREATE_BUSINESS_ROUTE)}
                        secondaryButtonText="Create Business"
                        title="Business Info"
                        subtitle="Summary"
                    >
                        <CardInfo
                            infoTexts={[
                                {
                                    text: `Businesses: ${
                                        getBusinessesByOwnerResponse?.businesses?.length ?? 0
                                    }`,
                                    icon: 'Home',
                                },
                                {
                                    text: `Employees: ${
                                        getBusinessesByOwnerResponse?.businesses?.flatMap(
                                            (business) => business.employees
                                        ).length ?? 0
                                    }`,
                                    icon: 'Hot',
                                },
                                {
                                    text: `Bookings: ${
                                        getBusinessesByOwnerResponse?.businesses?.flatMap(
                                            (business) => business.bookings
                                        ).length ?? 0
                                    }`,
                                    icon: 'Grain',
                                },
                            ]}
                        />
                    </InfoCard>
                </Grid>
            </Grid>
        </Box>
    );
};
