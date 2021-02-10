import {useState} from 'react';
import {Method} from 'axios';
import {ObjectSchema} from 'yup';
import {useFormik, FormikComputedProps, FormikHandlers, FormikHelpers, FormikState} from 'formik';

import AddressService from '../services/AddressService';
import {ComboBoxOption} from '../components/form/ComboBox';

export type FormHandler<T> = FormikState<T> &
  FormikComputedProps<T> &
  FormikHelpers<T> &
  FormikHandlers;

export type FormAddressHandler = {
  fetchAddresses: (zip: string | undefined) => Promise<ComboBoxOption[]>;
  fetchZips: () => Promise<ComboBoxOption[]>;
};

export type Form<T> = {
  formHandler: FormHandler<T>;
  addressHandler: FormAddressHandler;
  setRequest: (values: T) => void;
};

export const useForm = <T>(
  initialValues: T,
  validationSchema: ObjectSchema,
  url: string,
  method: Method,
  mutation: (url: string, method: Method, request: any) => Promise<T | null>,
  formatter?: (dto: T) => T,
  setUser?: (user: T) => void
): Form<T> => {
  const [request, setRequest] = useState<T | null>(null);

  const formHandler = useFormik<T>({
    initialValues,
    validationSchema,
    isInitialValid: false,
    onSubmit: async (values) => {
      if (formatter) values = formatter(values);

      console.log(values);

      const response = await mutation(url, method, request || values);

      if (setUser && response) setUser(response);
    },
  });

  const fetchZips = async (): Promise<ComboBoxOption[]> => {
    const zips = await AddressService.fetchZips();

    return zips.map((zip) => ({label: zip}));
  };

  const fetchAddresses = async (zip: string | undefined): Promise<ComboBoxOption[]> => {
    if (zip != undefined) {
      const addresses = await AddressService.fetchAddresses(zip);

      return addresses.map((address) => ({...address, label: address.address}));
    }

    return [];
  };

  return {
    formHandler,
    addressHandler: {fetchAddresses, fetchZips},
    setRequest,
  };
};
