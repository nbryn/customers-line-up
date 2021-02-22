import React, {useEffect, useState} from 'react';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory, useLocation} from 'react-router-dom';

import {ApiCaller, useApi} from '../../hooks/useApi';
import {BusinessDTO} from '../../models/Business';
import {Card} from '../../components/card/Card';
import {ComboBox, ComboBoxOption} from '../../components/form/ComboBox';
import DateUtil from '../../util/DateUtil';
import {ErrorView} from '../ErrorView';
import {Modal} from '../../components/modal/Modal';
import {NEW_TIMESLOT_URL} from '../../api/URL';

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

export const NewTimeSlotView: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    const location = useLocation<LocationState>();

    const [dateOptions, setDateOptions] = useState<ComboBoxOption[]>(DateUtil.getNext7Days());
    const [selectedDate, setSelectedDate] = useState<ComboBoxOption>();

    if (!location.state) {
        return <ErrorView />;
    }

    const business = location.state.business;

    const apiCaller: ApiCaller<void> = useApi(SUCCESS_MESSAGE);

    useEffect(() => {
        setDateOptions(dateOptions.filter((date) => date.label !== selectedDate?.label));
    }, [selectedDate]);

    return (
        <Row className={styles.row}>
            <Col lg={6}>
                <Modal
                    show={apiCaller.requestInfo ? true : false}
                    title={
                        apiCaller.requestInfo !== SUCCESS_MESSAGE
                            ? apiCaller.requestInfo
                            : selectedDate &&
                              `Time slots added on ${selectedDate.label.substring(
                                  selectedDate.label.indexOf(',') + 1
                              )}`
                    }
                    text={apiCaller.requestInfo}
                    primaryAction={() => history.push('/business/timeslots/manage', {business})}
                    primaryActionText="See time slots"
                    secondaryAction={() => apiCaller.setRequestInfo('')}
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
                        await apiCaller.mutation(NEW_TIMESLOT_URL, 'POST', {
                            BusinessId: business.id,
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
