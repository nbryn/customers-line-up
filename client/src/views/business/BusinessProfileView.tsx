import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';
import {useLocation} from 'react-router-dom';

import {BusinessDTO} from '../../models/Business';
import {businessValidationSchema} from '../../validation/BusinessValidation';
import {ExtendedCard, ExtendedCardData} from '../../components/card/ExtendedCard';
import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../hooks/useRequest';
import TextFieldUtil from '../../util/TextFieldUtil';
import {TextFieldModal} from '../../components/modal/TextFieldModal';
import URL, {BUSINESS_TYPES_URL} from '../../api/URL';
import {useForm} from '../../hooks/useForm';

const useStyles = makeStyles((theme) => ({
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

export const BusinessProfileView: React.FC = () => {
   const styles = useStyles();
   const location = useLocation<LocationState>();

   const [modalKey, setModalKey] = useState('');
   const [businessTypeOptions, setBusinessTypeOptions] = useState<string[]>([]);

   const requestHandler: RequestHandler<string[]> = useRequest();

   const business = location.state.business;

   useEffect(() => {
      (async () => {
         const types = await requestHandler.query(BUSINESS_TYPES_URL);

         setBusinessTypeOptions(types);
      })();
   }, []);

   const {formHandler} = useForm<BusinessDTO>(
      business,
      businessValidationSchema,
      URL.getUpdateBusinessDataURL(business.id),
      'PUT',
      requestHandler.mutation,
      (business) => {
         business.opens = business.opens.replace(':', '.');
         business.closes = business.closes.replace(':', '.');

         return business;
      }
   );

   const businessData: ExtendedCardData[] = Object.keys(business)
      .filter((x) => x !== 'id')
      .map((key) => ({
         text: TextFieldUtil.mapKeyToLabel(key),
         data: formHandler.values[key],
         buttonText: 'Edit',
         buttonAction: () => {
            setModalKey(key);
         },
      }));

   return (
      <>
         <Row className={styles.row}>
            <Header text={`Manage ${business.name}`} />
         </Row>
         <Row className={styles.row}>
            <TextFieldModal
               show={modalKey ? true : false}
               showModal={setModalKey}
               textFieldKey={modalKey}
               textFieldType={TextFieldUtil.mapKeyToType(modalKey)}
               primaryAction={async () => {
                  await formHandler.handleSubmit();
                  setModalKey('');
               }}
               formHandler={formHandler}
               selectOptions={businessTypeOptions}
               primaryActionText="Save Changes"
               secondaryAction={() => setModalKey('')}
            />

            <Col sm={12} md={6} lg={6} className={styles.col}>
               <ExtendedCard
                  title="General"
                  data={businessData.filter(
                     (x) => x.text === 'Name' || x.text === 'Zip' || x.text === 'Address' || x.text === 'Type'
                  )}
               />
            </Col>
            <Col sm={12} md={6} lg={6} className={styles.col}>
               <ExtendedCard title="Customer" data={businessData.filter(
                     (x) => x.text === 'Capacity' || x.text === 'Visit Length' || x.text === 'Opens' || x.text === 'Closes'
                  )} />
            </Col>
         </Row>
      </>
   );
};
