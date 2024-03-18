import React from 'react';
import Button from '@mui/material/Button';
import {Col, Row} from 'react-bootstrap';
import CircularProgress from '@mui/material/CircularProgress';
import makeStyles from '@mui/styles/makeStyles';
import {useHistory} from 'react-router-dom';

import {CardInfo} from '../../shared/components/card/CardInfo';
import {selectBusinessesByOwner} from './BusinessState';
import {Header, HeaderSize} from '../../shared/components/Texts';
import {InfoCard} from '../../shared/components/card/InfoCard';
import {selectApiState} from '../../shared/api/ApiState';
import {setCurrentBusiness} from './BusinessState';
import {useAppDispatch, useAppSelector} from '../../app/Store';

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
    const history = useHistory();
    const dispatch = useAppDispatch();

    const apiState = useAppSelector(selectApiState);
    const businesses = useAppSelector(selectBusinessesByOwner);
    return (
        <>
            <Row className={styles.row}>
                <Header
                    text={
                        businesses.length ? 'Choose Business' : 'You dont have any businesses yet'
                    }
                    size={HeaderSize.H1}
                />
            </Row>
            {apiState.loading ? (
                <Row className={styles.spinner}>
                    <CircularProgress />
                </Row>
            ) : (
                <Row className={styles.row}>
                    {!businesses.length ? (
                        <Row>
                            <Button
                                className={styles.noBusinessesButton}
                                variant="contained"
                                color="primary"
                                onClick={() => history.push('/business/new')}
                                size="small"
                            >
                                Create Business
                            </Button>
                        </Row>
                    ) : (
                        businesses.map((business) => {
                            return (
                                <Col key={business.id} sm={6} md={8} lg={4}>
                                    <InfoCard
                                        title={business.name}
                                        buttonText="Select Business"
                                        buttonAction={() => {
                                            dispatch(setCurrentBusiness(business));
                                            history.push('/business/overview');
                                        }}
                                    >
                                        <CardInfo
                                            infoTexts={[
                                                {text: `${business.zip}`, icon: 'City'},
                                                {text: `${business.street}`, icon: 'Home'},
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
