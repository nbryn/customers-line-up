import { Method } from "axios";
import { ObjectSchema } from 'yup';
import { useState } from 'react';
import { useFormik, FormikComputedProps, FormikHandlers, FormikState } from 'formik';

export type Form<T> = {
    formik: FormikState<T> & FormikComputedProps<T> & FormikHandlers;
    errorMessage: string;
}

export const useForm = <T>(initialValues: T, validationSchema: ObjectSchema,
    onSubmit: (url: string, method: Method, request?: any) => Promise<void>,
    url: string, 
    method: Method, 
    errorMsg: string): Form<T> => {     
    const [errorMessage, setErrorMessage] = useState<string>('');

    const formik = useFormik<T>({
        initialValues,
        validationSchema,
        onSubmit: async (values) => {
            try {

                await onSubmit(url, method, values);
            } catch (err) {
                console.log(err);
                setErrorMessage(errorMsg);
            }
        },
    });

    return {
        formik,
        errorMessage
    };
}