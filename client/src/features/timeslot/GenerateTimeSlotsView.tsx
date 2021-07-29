import React, {useEffect, useState} from 'react';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory, useLocation} from 'react-router-dom';

import {BusinessDTO} from '../business/Business';
import {Card} from '../../common/components/card/Card';
import {
    clearApiMessage,
    generateTimeSlots,
    TIMESLOTS_GENERATED_MSG,
} from '../timeslot/timeSlotSlice';
import {ComboBox, ComboBoxOption} from '../../common/components/form/ComboBox';
import DateUtil from '../../common/util/DateUtil';
import {State, selectApiMessage, useAppDispatch, useAppSelector} from '../../app/Store';
import {ErrorView} from '../../common/views/ErrorView';
import {Modal} from '../../common/components/modal/Modal';

const useStyles = makeStyles((theme) => ({
    button: {
        marginTop: 100,
        width: '55%',
    },
    card: {
        marginTop: 60,
        borderRadius: 15,
        height: 400,
        textAlign: 'center',
    },
    col: {
        marginTop: 25,
    },
    row: {
        justifyContent: 'center',
    },
}));

interface LocationState {
    business: BusinessDTO;
}

export const GenerateTimeSlotsView: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    const location = useLocation<LocationState>();

    const dispatch = useAppDispatch();
    const apiMessage = useAppSelector(selectApiMessage(State.TimeSlots));

    const [dateOptions, setDateOptions] = useState<ComboBoxOption[]>(DateUtil.getNext7Days());
    const [selectedDate, setSelectedDate] = useState<ComboBoxOption>();

    if (!location.state) {
        return <ErrorView />;
    }

    const business = location.state.business;

    useEffect(() => {
        setDateOptions(dateOptions.filter((date) => date.label !== selectedDate?.label));
    }, [selectedDate]);

    return (
        <Row className={styles.row}>
            <Col lg={6}>
                <Modal
                    show={apiMessage ? true : false}
                    title={
                        apiMessage !== TIMESLOTS_GENERATED_MSG
                            ? apiMessage
                            : selectedDate &&
                              `Time slots added on ${selectedDate.label.substring(
                                  selectedDate.label.indexOf(',') + 1
                              )}`
                    }
                    text={apiMessage}
                    primaryAction={() => {
                        dispatch(clearApiMessage());
                        history.push('/business/timeslots/manage', {business});
                    }}
                    primaryActionText="See time slots"
                    secondaryAction={() => dispatch(clearApiMessage())}
                />
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
    );
};
