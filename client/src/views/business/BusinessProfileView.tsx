import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';
import {useLocation} from 'react-router-dom';

import {BusinessDTO} from '../../dto/Business';
import {businessValidationSchema} from '../../validation/BusinessValidation';
import {ExtendedCard, ExtendedCardData} from '../../components/ExtendedCard';
import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import TextFieldUtil from '../../util/TextFieldUtil';
import {TextFieldModal, TextFieldType} from '../../components/TextFieldModal';
import URL, {BUSINESS_TYPES_URL} from '../../api/URL';
import {useForm} from '../../validation/useForm';

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
   const [textFieldType, setTextFieldType] = useState<TextFieldType>();
   const [businessTypeOptions, setBusinessTypeOptions] = useState<string[]>([]);

   const requestHandler: RequestHandler<string[]> = useRequest();

   const business = location.state.business;

   useEffect(() => {
      (async () => {
         const types = await requestHandler.query(BUSINESS_TYPES_URL);

         setBusinessTypeOptions(types);
      })();
   }, []);

   const form = useForm<BusinessDTO>(
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
      .map((x) => ({
         text: TextFieldUtil.mapKeyToLabel(x),
         data: form.values[x],
         buttonText: 'Edit',
         buttonAction: () => {
            setModalKey(x);
            setTextFieldType(TextFieldUtil.mapKeyToType(x));
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
               textFieldType={textFieldType}
               primaryAction={async () => {
                  await form.handleSubmit();
                  setModalKey('');
               }}
               form={form}
               selectOptions={businessTypeOptions}
               primaryActionText="Save Changes"
               secondaryAction={() => setModalKey('')}
            />

            <Col sm={12} md={6} lg={6} className={styles.col}>
               <ExtendedCard title="General" data={businessData.slice(0, 4)} />
            </Col>
            <Col sm={12} md={6} lg={6} className={styles.col}>
               <ExtendedCard title="Customer" data={businessData.slice(4)} />
            </Col>
         </Row>
      </>
   );
};
