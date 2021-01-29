import React, {useEffect, useState} from 'react';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory, useLocation} from 'react-router-dom';

import {BusinessDTO} from '../../models/Business';
import {Card} from '../../components/card/Card';
import {ComboBox, ComboBoxOption} from '../../components/form/ComboBox';
import DateUtil from '../../util/DateUtil';
import {Modal} from '../../components/modal/Modal';
import {NEW_TIMESLOT_URL} from '../../api/URL';
import {RequestHandler, useRequest} from '../../hooks/useRequest';

const useStyles = makeStyles((theme) => ({
   card: {
      marginTop: 60,
      borderRadius: 15,
      height: 600,
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

const SUCCESS_MESSAGE = 'Time Slots Generated - Go to time slot overview';

export const NewTimeSlotView: React.FC = () => {
   const styles = useStyles();
   const history = useHistory();
   const location = useLocation<LocationState>();

   const [selectedDate, setSelectedDate] = useState<ComboBoxOption>();

   const business = location.state.business;

   const requestHandler: RequestHandler<void> = useRequest(SUCCESS_MESSAGE);

   useEffect(() => {
      (async () => {
         if (selectedDate) {
            console.log(selectedDate);
            await requestHandler.mutation(NEW_TIMESLOT_URL, 'POST', {
               BusinessId: business.id,
               start: selectedDate?.value,
            });
         }
      })();
   }, [selectedDate]);

   return (
      <Row className={styles.row}>
         <Col lg={6}>
            <Modal
               show={requestHandler.requestInfo ? true : false}
               title="TimeSlot Info"
               text={requestHandler.requestInfo}
               primaryAction={() => history.push('/business/timeslots/manage', {business})}
               primaryActionText="My Time Slots"
               secondaryAction={() => requestHandler.setRequestInfo('')}
            />
            <Card
               className={styles.card}
               title="Generate Time Slots"
               subtitle="This will generate time slots in opening hours on the selected date"
               variant="outlined"
            >
               <ComboBox
                  style={{marginTop: 10, marginLeft: 110, width: '60%'}}
                  label="Pick a date"
                  id="email"
                  type="text"
                  options={DateUtil.getNext7Days()}
                  setFieldValue={(option: ComboBoxOption) => setSelectedDate(option)}
                  partOfForm={false}
               />
            </Card>
         </Col>
      </Row>
   );
};
