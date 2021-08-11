import {Col, Row} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect} from 'react';
import {useHistory} from 'react-router-dom';

import {CardInfo} from '../../common/components/card/CardInfo';
import {fetchBusinessesByOwner, selectBusinessesByOwner} from './businessSlice';
import {Header} from '../../common/components/Texts';
import {InfoCard} from '../../common/components/card/InfoCard';
import PathUtil, {PathInfo} from '../../common/util/PathUtil';
import {selectApiState} from '../../common/api/apiSlice';
import {useAppDispatch, useAppSelector} from '../../app/Store'

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

    useEffect(() => {
        (async () => {
            dispatch(fetchBusinessesByOwner());
        })();
    }, []);

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
                            localStorage.setItem('business', JSON.stringify(business));
                            return (
                                <Col key={business.id} sm={6} md={8} lg={4}>
                                    <InfoCard
                                        title={business.name}
                                        buttonText={pathInfo.primaryButtonText}
                                        buttonAction={() =>
                                            history.push(`/business/${pathInfo.primaryPath}`, {
                                                business: business,
                                            })
                                        }
                                        secondaryButtonText={pathInfo.secondaryButtonText}
                                        secondaryAction={() =>
                                            history.push(`/business/${pathInfo.secondaryPath}`, {
                                                business: business,
                                            })
                                        }
                                    >
                                        <CardInfo
                                            infoTexts={[
                                                {text: `${business.zip}`, icon: 'City'},
                                                {text: `${business.address}`, icon: 'Home'},
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
