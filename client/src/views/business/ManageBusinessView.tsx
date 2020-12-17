import {Col, Row} from 'react-bootstrap';

import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';
import {useHistory, useLocation} from 'react-router-dom';

import URL from '../../api/URL';
import {BusinessDTO} from '../../models/dto/Business';
import {ExtendedCard, ExtendedCardData} from '../../components/ExtendedCard';
import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import StringUtil from '../../util/StringUtil';
import {TextFieldModal, TextFieldType} from '../../components/TextFieldModal';

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

const SUCCESS_MESSAGE = 'Data Updated';

export const ManageBusinessView: React.FC = () => {
   const styles = useStyles();
   const history = useHistory();
   const location = useLocation<LocationState>();

   const [modalTitle, setModalTitle] = useState('');
   const [modalValue, setModalValue] = useState<string | number>('');
   const [textFieldType, setTextFieldType] = useState<TextFieldType>()

   const requestHandler: RequestHandler<BusinessDTO[]> = useRequest(SUCCESS_MESSAGE);

   const business = location.state.business;

   const updateBusinessData = async () => {
      const dtoKey = StringUtil.mapLabelToDTOKey(modalTitle);

      business[dtoKey] = modalValue;

      await requestHandler.mutation(URL.getUpdateBusinessDataURL(business.id), 'PUT', business);

      setModalTitle('');
   };

   const coreInfo: ExtendedCardData[] = Object.keys(business)
      .filter((x) => x !== 'id')
      .map((x) => {
         const text = StringUtil.mapDTOKeyToLabel(x);
         return {
            text,
            data: business[x],
            buttonText: 'Edit',
            buttonAction: () => {
               setModalTitle(text);
               setModalValue(business[x] as string);
               setTextFieldType(StringUtil.getTextFieldTypeFromKey(x));
            },
         };
      });

   const manageInfo: ExtendedCardData[] = [
      {
         text: 'Employees',
         data: 0,
         buttonText: 'Manage',
         buttonAction: () => {
            history.push('/business/employees', {
               data: {id: business.id, name: business.name},
            });
         },
      },
      {
         text: 'Bookings',
         data: 0,
         buttonText: 'Manage',
         buttonAction: () => {
            history.push('/business/bookings', {
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
               value={modalValue}
               textFieldType={textFieldType}
               setValue={setModalValue}
               primaryAction={() => updateBusinessData()}
               primaryActionText="Save Changes"
               secondaryAction={() => {
                  setModalTitle('');
                  setModalValue('');
               }}
            />
            <Col sm={12} md={6} lg={6} className={styles.col}>
               <ExtendedCard title="Manage" data={manageInfo} />
            </Col>
            <Col sm={12} md={6} lg={6} className={styles.col}>
               <ExtendedCard title="Business Data" data={coreInfo} />
            </Col>
         </Row>
      </>
   );
};
