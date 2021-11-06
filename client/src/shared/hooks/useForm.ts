import {useEffect, useState} from 'react';
import {ObjectSchema} from 'yup';
import {useFormik, FormikComputedProps, FormikHandlers, FormikHelpers, FormikState} from 'formik';

import AddressService from '../services/AddressService';
import {Address} from '../services/AddressService';
import {ComboBoxOption} from '../components/form/ComboBox';
import {Index} from '../models/General';

type AddressKey = 'zip' | 'street';

export type FormHandler<T> = FormikState<T> &
    FormikComputedProps<T> &
    FormikHelpers<T> &
    FormikHandlers;

export type AddressHandler = {
    addresses: Address[];
    fetchAddresses: (zip: string | undefined) => Promise<void>;
    fetchZips: () => Promise<void>;
    getLabels: (key: AddressKey) => ComboBoxOption[];
};

export type Form<T> = {
    formHandler: FormHandler<T>;
    addressHandler: AddressHandler;
    setRequest: (values: T) => void;
};

export type FormProps<T> = {
    initialValues: T;
    validationSchema: ObjectSchema;
    onSubmit: (values: T) => void;
    beforeSubmit?: (dto: T) => T;
};

export const useForm = <T extends Index>({
    initialValues,
    validationSchema,
    onSubmit,
    beforeSubmit,
}: FormProps<T>): Form<T> => {
    const [request, setRequest] = useState<T | null>(null);
    const [addresses, setAddresses] = useState<Address[]>([]);

    const formHandler = useFormik<T>({
        initialValues,
        validationSchema,
        isInitialValid: false,
        onSubmit: async (values) => {
            if (beforeSubmit) values = beforeSubmit(values);

            onSubmit(request || values);
        },
    });

    const fetchZips = async (): Promise<void> => {
        setAddresses(await AddressService.fetchZips());
    };

    const fetchAddresses = async (zip: string | undefined): Promise<void> => {
        if (zip) {
            const address = addresses.find((a) => a.zipCity === zip);
            const newAddresses = await AddressService.fetchAddresses(address);

            setAddresses(newAddresses);
        }
    };

    const getLabels = (key: AddressKey): ComboBoxOption[] => {
        return addresses.map((a) => ({
            label: key === 'zip' ? a.zipCity : a.street ?? 'Choose zip first',
        }));
    };

    useEffect(() => {
        (async () => {
            await fetchZips();
        })();
    }, []);

    useEffect(() => {
        (async () => {
            const {zip} = formHandler.values;
            await fetchAddresses(zip as string);
        })();
    }, [formHandler.values.zip]);

    return {
        formHandler,
        addressHandler: {addresses, fetchAddresses, fetchZips, getLabels},
        setRequest,
    };
};
