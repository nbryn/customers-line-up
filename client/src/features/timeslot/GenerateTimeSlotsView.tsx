import React, {useEffect, useState} from 'react';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory, useLocation} from 'react-router-dom';

import {BusinessDTO} from '../business/Business';
import {Card} from '../../common/components/card/Card';
import {ComboBox, ComboBoxOption} from '../../common/components/form/ComboBox';
import DateUtil from '../../common/util/DateUtil';
import {ErrorView} from '../../common/views/ErrorView';
import {Modal} from '../../common/components/modal/Modal';
import {useTimeSlotService} from './TimeSlotService';

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

const SUCCESS_MESSAGE = 'Success! Press see time slots to manage time slots.';

export const GenerateTimeSlotsView: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    const location = useLocation<LocationState>();

    const [dateOptions, setDateOptions] = useState<ComboBoxOption[]>(DateUtil.getNext7Days());
    const [selectedDate, setSelectedDate] = useState<ComboBoxOption>();

    if (!location.state) {
        return <ErrorView />;
    }

    const business = location.state.business;

    const timeSlotService = useTimeSlotService(SUCCESS_MESSAGE);

    useEffect(() => {
        setDateOptions(dateOptions.filter((date) => date.label !== selectedDate?.label));
    }, [selectedDate]);

    return (
        <Row className={styles.row}>
            <Col lg={6}>
                <Modal
                    show={timeSlotService.requestInfo ? true : false}
                    title={
                        timeSlotService.requestInfo !== SUCCESS_MESSAGE
                            ? timeSlotService.requestInfo
                            : selectedDate &&
                              `Time slots added on ${selectedDate.label.substring(
                                  selectedDate.label.indexOf(',') + 1
                              )}`
                    }
                    text={timeSlotService.requestInfo}
                    primaryAction={() => history.push('/business/timeslots/manage', {business})}
                    primaryActionText="See time slots"
                    secondaryAction={() => timeSlotService.setRequestInfo('')}
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
                    disableButton={!selectedDate || dateOptions.length === 0 ? true : false}
                    buttonAction={async () =>
                        await timeSlotService.generateTimeSlots({
                            businessId: business.id,
                            start: selectedDate?.value,
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
    );
};
