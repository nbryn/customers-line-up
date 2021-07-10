import {useState} from 'react';
import {ObjectSchema} from 'yup';
import {useFormik, FormikComputedProps, FormikHandlers, FormikHelpers, FormikState} from 'formik';
import {AsyncThunk} from '@reduxjs/toolkit';

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

export type FormProps<T> = {
    initialValues: T;
    validationSchema: ObjectSchema;
    onSubmit: (data: any) => Promise<T | void>;
    formatter?: (dto: T) => T;
    setUser?: (user: T) => void;
};

export const useForm = <T>({
    initialValues,
    validationSchema,
    onSubmit,
    formatter,
    setUser,
}: FormProps<T>): Form<T> => {
    const [request, setRequest] = useState<T | null>(null);

    const formHandler = useFormik<T>({
        initialValues,
        validationSchema,
        isInitialValid: false,
        onSubmit: async (values) => {
            if (formatter) values = formatter(values);

            const response = await onSubmit(request || values);

            if (setUser && response) setUser(response);
        },
    });

    const fetchZips = async (): Promise<ComboBoxOption[]> => {
        const zips = await AddressService.fetchZips();

        return zips.map((zip) => ({label: zip}));
    };

    const fetchAddresses = async (zip: string | undefined): Promise<ComboBoxOption[]> => {
        if (zip) {
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
