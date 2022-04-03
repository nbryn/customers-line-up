import React from 'react';
import {Col, Row} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory} from 'react-router-dom';

import {CardInfo} from '../../shared/components/card/CardInfo';
import {selectBusinessesByOwner} from './BusinessState';
import {Header} from '../../shared/components/Texts';
import {InfoCard} from '../../shared/components/card/InfoCard';
import PathUtil, {PathInfo} from '../../shared/util/PathUtil';
import {selectApiState} from '../../shared/api/ApiState';
import {setCurrentBusiness} from './BusinessState';
import {useAppDispatch, useAppSelector} from '../../app/Store';

const useStyles = makeStyles((theme) => ({
    row: {
        justifyContent: 'center',
    },
    spinner: {
        justifyContent: 'center',
        marginTop: 100,
    },
}));

export const BusinessOverview: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    const dispatch = useAppDispatch();

    const apiState = useAppSelector(selectApiState);
    const businesses = useAppSelector(selectBusinessesByOwner);
    const pathInfo: PathInfo = PathUtil.getPathAndTextFromURL(window.location.pathname);

    return (
        <>
            <Row className={styles.row}>
                <Header text="Choose Business" />
            </Row>
            {apiState.loading ? (
                <Row className={styles.spinner}>
                    <CircularProgress />
                </Row>
            ) : (
                <Row className={styles.row}>
                    <>
                        {businesses.map((business) => {
                            return (
                                <Col key={business.id} sm={6} md={8} lg={4}>
                                    <InfoCard
                                        title={business.name}
                                        buttonText={pathInfo.primaryButtonText}
                                        buttonAction={() => {
                                            dispatch(setCurrentBusiness(business));
                                            history.push(`/business/${pathInfo.primaryPath}`);
                                        }}
                                        secondaryButtonText={pathInfo.secondaryButtonText}
                                        secondaryAction={() => {
                                            dispatch(setCurrentBusiness(business));
                                            history.push(`/business/${pathInfo.secondaryPath}`);
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
                        })}
                    </>
                </Row>
            )}
        </>
    );
};
