import {Col, Row} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';
import {useHistory} from 'react-router-dom';

import {CardInfo} from '../../common/components/card/CardInfo';
import {BusinessDTO} from './Business';
import {HomeCard} from '../../common/components/card/HomeCard';
import PathUtil, {PathInfo} from '../../common/util/PathUtil';
import {Header} from '../../common/components/Texts';
import {useBusinessService} from './BusinessService';

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

    const [businessData, setBusinessData] = useState<BusinessDTO[]>([]);

    const pathInfo: PathInfo = PathUtil.getPathAndTextFromURL(window.location.pathname);

    const businessService = useBusinessService();

    useEffect(() => {
        (async () => {
            const businesses: BusinessDTO[] = await businessService.fetchBusinesssesByOwner();

            setBusinessData(businesses);
        })();
    }, []);

    return (
        <>
            <Row className={styles.row}>
                <Header text="Choose Business" />
            </Row>
            <Row className={styles.row}>
                {businessService.working && <CircularProgress />}
                <>
                    {businessData.map((business) => {
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
