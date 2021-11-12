import {useState} from 'react';
import {ObjectSchema} from 'yup';
import {useFormik, FormikComputedProps, FormikHandlers, FormikHelpers, FormikState} from 'formik';

import {Index} from '../models/General';

export type FormHandler<T> = FormikState<T> &
    FormikComputedProps<T> &
    FormikHelpers<T> &
    FormikHandlers;

export type Form<T> = {
    formHandler: FormHandler<T>;
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

    const formHandler = useFormik<T>({
        initialValues,
        validationSchema,
        isInitialValid: false,
        onSubmit: async (values) => {
            if (beforeSubmit) values = beforeSubmit(values);

            onSubmit(request || values);
        },
    });

    return {
        formHandler,
        setRequest,
    };
};
