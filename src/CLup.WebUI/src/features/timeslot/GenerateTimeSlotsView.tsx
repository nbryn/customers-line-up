import React, {useEffect, useState} from 'react';
import {useParams} from 'react-router-dom';
import {Col, Row} from 'react-bootstrap';
import makeStyles from '@mui/styles/makeStyles';

import {Card} from '../../shared/components/card/Card';
import {useGenerateTimeSlotsMutation} from './TimeSlotApi';
import {ComboBox} from '../../shared/components/form/ComboBox';
import type {ComboBoxOption} from '../../shared/components/form/ComboBox';
import DateUtil from '../../shared/util/DateUtil';
import {Header} from '../../shared/components/Texts';
import {useGetBusinessAggregateByIdQuery} from '../business/BusinessApi';

const useStyles = makeStyles(() => ({
    button: {
        marginTop: 100,
        width: '55%',
    },
    card: {
        borderRadius: 15,
        height: 400,
        textAlign: 'center',
    },
    row: {
        justifyContent: 'center',
    },
}));

export const GenerateTimeSlotsView: React.FC = () => {
    const [dateOptions, setDateOptions] = useState<ComboBoxOption[]>(DateUtil.getNext7Days());
    const [selectedDate, setSelectedDate] = useState<ComboBoxOption>();

    const {businessId} = useParams();
    const styles = useStyles();

    const {data: business} = useGetBusinessAggregateByIdQuery(businessId!);
    const [generateTimeSlots] = useGenerateTimeSlotsMutation();

    useEffect(() => {
        setDateOptions(dateOptions.filter((date) => date.label !== selectedDate?.label));
    }, [selectedDate]);

    return (
        <>
            <Row className={styles.row}>
                <Header text={business?.name ?? ''} />
            </Row>
            <Row className={styles.row}>
                <Col lg={6}>
                    <Card
                        className={styles.card}
                        title="Generate Time Slots"
                        subtitle="This will generate time slots in opening hours on the selected date"
                        variant="outlined"
                        buttonText="Generate"
                        buttonColor="primary"
                        buttonStyle={styles.button}
                        buttonSize="large"
                        disableButton={!selectedDate || !dateOptions.length}
                        buttonAction={async () =>
                            await generateTimeSlots({
                                businessId: business?.id ?? '',
                                date: {
                                    year: selectedDate?.date?.year(),
                                    month: selectedDate?.date?.month(),
                                    day: selectedDate?.date?.date(),
                                },
                            })
                        }
                    >
                        <ComboBox
                            style={{marginTop: 10, marginLeft: 110, width: '60%'}}
                            label="Pick a date"
                            defaultLabel="Time slots already generated"
                            id="email"
                            type="text"
                            options={dateOptions}
                            setFieldValue={(option: ComboBoxOption) => setSelectedDate(option)}
                        />
                    </Card>
                </Col>
            </Row>
        </>
    );
};
