import {Col, Row} from 'react-bootstrap';

import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';
import {useHistory, useLocation} from 'react-router-dom';

import {BusinessDTO} from '../../dto/Business';
import {businessValidationSchema} from '../../validation/BusinessValidation';
import {ExtendedCard, ExtendedCardData} from '../../components/ExtendedCard';
import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import StringUtil from '../../util/StringUtil';
import {TextFieldModal, TextFieldType} from '../../components/TextFieldModal';
import URL, {BUSINESS_TYPES_URL} from '../../api/URL';
import {useForm} from '../../util/useForm';

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

export const ManageBusinessView: React.FC = () => {
   const styles = useStyles();
   const history = useHistory();
   const location = useLocation<LocationState>();

   const [modalTitle, setModalTitle] = useState('');
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
      requestHandler.mutation,
      URL.getUpdateBusinessDataURL(business.id),
      'PUT',
      (business) => {
         business.opens = business.opens.replace(':', '.');
         business.closes = business.closes.replace(':', '.');

         return business;
      }
   );

   const businessData: ExtendedCardData[] = Object.keys(business)
      .filter((x) => x !== 'id')
      .map((x) => {
         const text = StringUtil.mapDTOKeyToLabel(x);
         return {
            text: StringUtil.mapDTOKeyToLabel(x),
            data: form.values[x],
            buttonText: 'Edit',
            buttonAction: () => {
               setModalTitle(text);
               setTextFieldType(StringUtil.getTextFieldTypeFromKey(x));
            },
         };
      });

   const manageInfo: ExtendedCardData[] = [
      {
         text: 'Bookings',
         data: 0,
         buttonText: 'Manage',
         buttonAction: () => {
            history.push('/business/bookings/manage', {
               data: {id: business.id, name: business.name},
            });
         },
      },
      {
         text: 'Time Slots',
         data: 0,
         buttonText: 'Manage',
         buttonAction: () => {
            history.push('/business/timeslots/manage', {
               data: {id: business.id, name: business.name},
            });
         },
      },
      {
         text: 'Employees',
         data: 0,
         buttonText: 'Manage',
         buttonAction: () => {
            history.push('/business/employees/manage', {
               data: {id: business.id, name: business.name},
            });
         },
      },
   ];

   return (
      <>
         <Row className={styles.row}>
            <Header text={`Manage ${business.name}`} />
         </Row>
         <Row className={styles.row}>
            <TextFieldModal
               show={modalTitle ? true : false}
               title={`Edit ${modalTitle}`}
               valueLabel={modalTitle}
               id={StringUtil.unCapitalizeFirstLetter(modalTitle)}
               textFieldType={textFieldType}
               primaryAction={async () => {
                  await form.handleSubmit();
                  setModalTitle('');
               }}
               form={form}
               selectOptions={businessTypeOptions}
               primaryActionText="Save Changes"
               secondaryAction={() => setModalTitle('')}
            />
            <Col sm={12} md={6} lg={6} className={styles.col}>
               <ExtendedCard title="Manage" data={manageInfo} />
            </Col>
            <Col sm={12} md={6} lg={6} className={styles.col}>
               <ExtendedCard title="Business Data" data={businessData} />
            </Col>
         </Row>
      </>
   );
};
