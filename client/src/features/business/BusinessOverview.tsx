import {Col, Row} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect} from 'react';
import {useHistory} from 'react-router-dom';

import {CardInfo} from '../../common/components/card/CardInfo';
import {fetchBusinessesByOwner, selectBusinessesByOwner} from './businessSlice';
import {HomeCard} from '../../common/components/card/HomeCard';
import {isLoading, State, useAppDispatch, useAppSelector} from '../../app/Store';
import PathUtil, {PathInfo} from '../../common/util/PathUtil';
import {Header} from '../../common/components/Texts';

const useStyles = makeStyles((theme) => ({
    icon: {
        marginBottom: 10,
        marginLeft: -35,
    },
    row: {
        justifyContent: 'center',
    },
}));

export const BusinessOverview: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    const dispatch = useAppDispatch();

    const loading = useAppSelector(isLoading(State.Businesses))
    const businesses = useAppSelector(selectBusinessesByOwner)

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
            <Row className={styles.row}>
                {loading && <CircularProgress />}
                <>
                    {businesses.map((business) => {
                        localStorage.setItem('business', JSON.stringify(business));
                        return (
                            <Col key={business.id} sm={6} md={8} lg={4}>
                                <HomeCard
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
                                        icon1={{
                                            text: `City: ${business.zip}`,
                                            icon: 'City',
                                            styles: styles.icon,
                                        }}
                                        icon2={{
                                            text: `Address: ${business.address}`,
                                            icon: 'Home',
                                            styles: styles.icon,
                                        }}
                                    />
                                </HomeCard>
                            </Col>
                        );
                    })}
                </>
            </Row>
        </>
    );
};
