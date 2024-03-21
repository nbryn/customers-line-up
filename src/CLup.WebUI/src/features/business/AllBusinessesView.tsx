import React, {useState} from 'react';
import Chip from '@mui/material/Chip';
import {Col, Row} from 'react-bootstrap';
import makeStyles from '@mui/styles/makeStyles';
import {useHistory} from 'react-router';

import type {BusinessDTO} from './Business';
import {fetchAllBusinesses, selectAllBusinesses, setCurrentBusiness} from './BusinessApi';
import {Header} from '../../shared/components/Texts';
import {MapModal, defaultMapProps} from '../../shared/components/modal/MapModal';
import type {MapModalProps} from '../../shared/components/modal/MapModal';
import type {TableColumn} from '../../shared/components/Table';
import {TableContainer} from '../../shared/containers/TableContainer';
import {useAppDispatch, useAppSelector} from '../../app/Store';

const useStyles = makeStyles(() => ({
    row: {
        justifyContent: 'center',
    },
}));

export const AllBusinessesView: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    const dispatch = useAppDispatch();

    const businesses = useAppSelector(selectAllBusinesses);
    const [mapModalInfo, setMapModalInfo] = useState<MapModalProps>(defaultMapProps);

    const columns: TableColumn[] = [
        {title: 'id', field: 'id', hidden: true},
        {title: 'Name', field: 'name'},
        {title: 'City', field: 'city'},
        {title: 'Street', field: 'street'},
        {title: 'Business Hours', field: 'businessHours'},
        {title: 'Type', field: 'type'},
    ];

    const actions = [
        {
            icon: () => <Chip size="small" label="Go to business" clickable color="primary" />,
            tooltip: 'See available time slots',
            onClick: (event: any, business: BusinessDTO) => {
                dispatch(setCurrentBusiness(business));
                history.push('/booking/new', {business});
            },
        },
        {
            icon: () => <Chip size="small" label="See on map" clickable color="secondary" />,
            tooltip: 'Show location on map',
            onClick: (event: any, business: BusinessDTO) => {
                setMapModalInfo({
                    visible: true,
                    zoom: 14,
                    center: [business.longitude as number, business.latitude as number],
                    markers: [[business.longitude as number, business.latitude as number], 13],
                });
            },
        },
    ];

    return (
        <>
            <Row className={styles.row}>
                <Header text="Available Businesses" />
            </Row>
            <MapModal
                visible={mapModalInfo.visible}
                setVisible={() => setMapModalInfo(defaultMapProps)}
                zoom={mapModalInfo.zoom}
                center={mapModalInfo.center}
                markers={mapModalInfo.markers}
            />
            <Row className={styles.row}>
                <Col sm={6} md={8} xl={12}>
                    <TableContainer
                        actions={actions}
                        columns={columns}
                        tableData={businesses}
                        fetchData={() => dispatch(fetchAllBusinesses())}
                        tableTitle="Businesses"
                    />
                </Col>
            </Row>
        </>
    );
};
