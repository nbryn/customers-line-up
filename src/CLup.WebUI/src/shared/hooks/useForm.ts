import {useState} from 'react';
import type {ObjectSchema} from 'yup';
import type {FormikComputedProps, FormikHandlers, FormikHelpers, FormikState} from 'formik';
import {useFormik} from 'formik';

import type {Index} from '../models/General';
import type {FetchBaseQueryError} from '@reduxjs/toolkit/query';
import type {SerializedError} from '@reduxjs/toolkit';

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
    // TODO: Fix this weird function signature
    onSubmit: (values: T) => Promise<{data: void} | {error: FetchBaseQueryError | SerializedError}>;
    beforeSubmit?: (request: T) => T;
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

            await onSubmit(request || values);
        },
    });

    return {
        formHandler,
        setRequest,
    };
};
