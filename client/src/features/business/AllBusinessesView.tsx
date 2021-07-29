import React, {useState} from 'react';
import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory} from 'react-router';

import {BusinessDTO} from './Business';
import {fetchAllBusinesses, selectAllBusinesses} from './businessSlice';
import {Header} from '../../common/components/Texts';
import {isLoading, State, useAppDispatch, useAppSelector} from '../../app/Store';
import {MapModal, MapModalProps, defaultMapProps} from '../../common/components/modal/MapModal';
import {TableColumn} from '../../common/components/Table';
import {TableContainer} from '../../common/containers/TableContainer';

const useStyles = makeStyles((theme) => ({
    headline: {
        marginTop: 75,
        justifyContent: 'center',
    },
    row: {
        justifyContent: 'center',
    },
}));

export const AllBusinessesView: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    const dispatch = useAppDispatch();

    const loading = useAppSelector(isLoading(State.Businesses));
    const businesses = useAppSelector(selectAllBusinesses);
    const [mapModalInfo, setMapModalInfo] = useState<MapModalProps>(defaultMapProps);

    const columns: TableColumn[] = [
        {title: 'id', field: 'id', hidden: true},
        {title: 'Name', field: 'name'},
        {title: 'City', field: 'city'},
        {title: 'Address', field: 'address'},
        {title: 'Business Hours', field: 'businessHours'},
        {title: 'Type', field: 'type'},
    ];

    const actions = [
        {
            icon: () => <Chip size="small" label="Go to business" clickable color="primary" />,
            tooltip: 'See available time slots',
            onClick: (event: any, business: BusinessDTO) => {
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
            <Row className={styles.headline}>
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
                        loading={loading}
                        tableData={businesses}
                        fetchData={() => dispatch(fetchAllBusinesses())}
                        tableTitle="Businesses"
                    />
                </Col>
            </Row>
        </>
    );
};
