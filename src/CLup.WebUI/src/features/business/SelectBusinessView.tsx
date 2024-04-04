import React from 'react';
import {useNavigate} from 'react-router-dom';
import Button from '@mui/material/Button';
import {Col, Row} from 'react-bootstrap';
import CircularProgress from '@mui/material/CircularProgress';
import makeStyles from '@mui/styles/makeStyles';

import {CardInfo} from '../../shared/components/card/CardInfo';
import {useGetBusinessesByOwnerQuery} from './BusinessApi';
import {setCurrentBusiness} from './BusinessState';
import {Header, HeaderSize} from '../../shared/components/Texts';
import {InfoCard} from '../../shared/components/card/InfoCard';
import {isLoading} from '../../shared/api/ApiState';
import {useAppDispatch, useAppSelector} from '../../app/Store';
import {BUSINESS_OVERVIEW_ROUTE, CREATE_BUSINESS_ROUTE} from '../../app/RouteConstants';

const useStyles = makeStyles(() => ({
    noBusinessesButton: {
        justifyContent: 'center',
        top: 100,
        height: 50,
        width: '100%',
    },
    row: {
        justifyContent: 'center',
    },
    spinner: {
        justifyContent: 'center',
        marginTop: 100,
    },
}));

export const SelectBusinessView: React.FC = () => {
    const styles = useStyles();
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const loading = useAppSelector(isLoading);

    const {data: businessesByOwnerResponse} = useGetBusinessesByOwnerQuery();
    return (
        <>
            <Row className={styles.row}>
                <Header
                    text={
                        businessesByOwnerResponse?.businesses?.length
                            ? 'Choose Business'
                            : 'You dont have any businesses yet'
                    }
                    size={HeaderSize.H1}
                />
            </Row>
            {loading ? (
                <Row className={styles.spinner}>
                    <CircularProgress />
                </Row>
            ) : (
                <Row className={styles.row}>
                    {!businessesByOwnerResponse?.businesses?.length ? (
                        <Row>
                            <Button
                                className={styles.noBusinessesButton}
                                variant="contained"
                                color="primary"
                                onClick={() => navigate(CREATE_BUSINESS_ROUTE)}
                                size="small"
                            >
                                {'Create Business'}
                            </Button>
                        </Row>
                    ) : (
                        businessesByOwnerResponse?.businesses.map((business) => {
                            return (
                                <Col key={business.id} sm={6} md={8} lg={4}>
                                    <InfoCard
                                        title={business.name ?? ''}
                                        buttonText="Select Business"
                                        buttonAction={() => {
                                            dispatch(setCurrentBusiness(business));
                                            navigate(BUSINESS_OVERVIEW_ROUTE);
                                        }}
                                    >
                                        <CardInfo
                                            infoTexts={[
                                                {text: `${business.address?.zip}`, icon: 'City'},
                                                {text: `${business.address?.street}`, icon: 'Home'},
                                            ]}
                                        />
                                    </InfoCard>
                                </Col>
                            );
                        })
                    )}
                </Row>
            )}
        </>
    );
};
