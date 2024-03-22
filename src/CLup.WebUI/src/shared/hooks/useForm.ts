import {useState} from 'react';
import type {ObjectSchema} from 'yup';
import type {FormikComputedProps, FormikHandlers, FormikHelpers, FormikState} from 'formik';
import {useFormik} from 'formik';

export interface Index {
    [key: string]: string | number | boolean | undefined;
}

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
    // TODO: Remove any?
    onSubmit: (values: T) => Promise<any>;
};

export const useForm = <T extends Index>({
    initialValues,
    validationSchema,
    onSubmit,
}: FormProps<T>): Form<T> => {
    const [request, setRequest] = useState<T | null>(null);

    const formHandler = useFormik<T>({
        initialValues,
        validationSchema,
        isInitialValid: false,
        onSubmit: async (values) => await onSubmit(request ?? values),
    });

    return {
        formHandler,
        setRequest,
    };
};
