import React, {useState} from 'react';
import {useParams} from 'react-router-dom';
import Chip from '@mui/material/Chip';
import makeStyles from '@mui/styles/makeStyles';

import {useCreateBookingMutation} from './BookingApi';
import {Header} from '../../shared/components/Texts';
import {MapModal} from '../../shared/components/modal/MapModal';
import {Table, type TableColumn} from '../../shared/components/Table';
import type {TimeSlotDto} from '../../autogenerated';
import {mapTimeSlotsToTableData} from '../timeslot/TimeSlotView';
import {Box, Grid} from '@mui/material';
import {useGetBusinessByIdQuery} from '../business/BusinessApi';

const useStyles = makeStyles({
    address: {
        marginTop: 10,
    },
    badge: {
        marginTop: -10,
        marginLeft: 35,
    },
    box: {
        alignItems: 'center',
        display: 'flex',
        flexDirection: 'column',
    },
    row: {
        justifyContent: 'center',
    },
});

export const CreateBookingView: React.FC = () => {
    const [showMapModal, setShowMapModal] = useState<boolean>(false);
    const [createBooking] = useCreateBookingMutation();
    const {businessId} = useParams();
    const styles = useStyles();

    const {data: business} = useGetBusinessByIdQuery(businessId!);
    const timeSlots = business?.timeSlots?.filter((timeSlot) => timeSlot.available) ?? [];
    
    const columns: TableColumn[] = [
        {title: 'id', field: 'id', hidden: true},
        {title: 'Date', field: 'date'},
        {title: 'Interval', field: 'interval'},
        {title: 'Capacity', field: 'capacity'},
    ];

    const actions = [
        {
            icon: () => <Chip size="small" label="Book" clickable color="primary" />,
            onClick: async (_: any, rowData: Partial<TimeSlotDto>) => {
                console.log(rowData);
                await createBooking({
                    timeSlotId: rowData.id ?? '',
                    businessId: business?.id ?? '',
                });
            },
        },
    ];

    const address = business?.address;
    return (
        <Box className={styles.box}>
            <Header text={'Create Booking'} />

            <Grid container justifyContent="center">
                <MapModal
                    open={showMapModal}
                    title={`${business?.name} - ${address?.city}`}
                    setVisible={() => setShowMapModal(false)}
                    zoom={14}
                    center={[
                        address?.coords?.longitude as number,
                        address?.coords?.latitude as number,
                    ]}
                    markers={[
                        [address?.coords?.longitude as number, address?.coords?.latitude as number],
                        13,
                    ]}
                />
                <Grid item xs={6}>
                    <Table
                        actions={actions}
                        columns={columns}
                        data={mapTimeSlotsToTableData(timeSlots)}
                        title={
                            <>
                                <h5 className={styles.address}>{`${business?.name}: ${
                                    address?.street ?? ''
                                }, ${address?.city ?? ''}`}</h5>
                                <Chip
                                    className={styles.badge}
                                    size="small"
                                    label="Open map"
                                    color="primary"
                                    onClick={() => setShowMapModal(true)}
                                    clickable
                                />
                            </>
                        }
                        emptyMessage="No Time Slots Available"
                    />
                </Grid>
            </Grid>
        </Box>
    );
};
