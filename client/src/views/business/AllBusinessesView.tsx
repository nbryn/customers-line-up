import React, {useState} from 'react';
import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory} from 'react-router';

import {ALL_BUSINESSES_URL} from '../../api/URL';
import {BusinessDTO} from '../../models/Business';
import {Header} from '../../components/Texts';
import {defaultMapProps, MapModal, MapModalProps} from '../../components/modal/MapModal';
import {RequestHandler, useRequest} from '../../hooks/useRequest';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';

const useStyles = makeStyles((theme) => ({
  row: {
    justifyContent: 'center',
  },
}));

export const AllBusinessesView: React.FC = () => {
  const styles = useStyles();
  const history = useHistory();

  const [mapModalInfo, setMapModalInfo] = useState<MapModalProps>(defaultMapProps);

  const requestHandler: RequestHandler<BusinessDTO[]> = useRequest();

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
            fetchTableData={async () => {
              const businesses = await requestHandler.query(ALL_BUSINESSES_URL);

              return businesses.map((business) => {
                const zipSplit: string[] = business.zip.split(' ');
                const city = zipSplit.length === 3 ? zipSplit[2] : `${zipSplit[2]} ${zipSplit[3]}`;

                return {
                  ...business,
                  city,
                  businessHours: business.opens + ' - ' + business.closes,
                };
              });
            }}
            tableTitle="Businesses"
          />
        </Col>
      </Row>
    </>
  );
};
