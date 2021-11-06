import React, {useEffect, useState} from 'react';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';

import {Card} from '../../../shared/components/card/Card';
import {generateTimeSlots} from './timeSlotSlice';
import {ComboBox, ComboBoxOption} from '../../../shared/components/form/ComboBox';
import DateUtil from '../../../shared/util/DateUtil';
import {ErrorView} from '../../../shared/views/ErrorView';
import {Header} from '../../../shared/components/Texts';
import {selectCurrentBusiness} from '../businessSlice';
import {useAppDispatch, useAppSelector} from '../../../app/Store';

const useStyles = makeStyles((theme) => ({
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
    const styles = useStyles();
    const dispatch = useAppDispatch();
    const business = useAppSelector(selectCurrentBusiness);

    const [dateOptions, setDateOptions] = useState<ComboBoxOption[]>(DateUtil.getNext7Days());
    const [selectedDate, setSelectedDate] = useState<ComboBoxOption>();

    if (!business) {
        return <ErrorView />;
    }

    useEffect(() => {
        setDateOptions(dateOptions.filter((date) => date.label !== selectedDate?.label));
    }, [selectedDate]);

    return (
        <>
            <Row className={styles.row}>
                <Header text={business.name} />
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
                        buttonAction={() =>
                            dispatch(
                                generateTimeSlots({
                                    businessId: business.id,
                                    start: selectedDate!.value!,
                                })
                            )
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
