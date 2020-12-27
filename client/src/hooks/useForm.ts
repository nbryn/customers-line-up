import { useState } from 'react';
import { Method } from "axios";
import { ObjectSchema } from 'yup';
import { useFormik, FormikComputedProps, FormikHandlers, FormikHelpers, FormikState } from 'formik';

export type FormHandler<T> = FormikState<T> & FormikComputedProps<T> & FormikHelpers<T> & FormikHandlers;

export type Form<T> = {
    formHandler: FormHandler<T>;
    setRequest: (values: T) => void;
}

export const useForm = <T>(
    initialValues: T,
    validationSchema: ObjectSchema,
    url: string,
    method: Method,
    mutation: (url: string, method: Method, request: any) => Promise<T | null>,
    formatter?: (dto: T) => T,
    setUser?: (user: T) => void,
): Form<T> => {
    const [request, setRequest] = useState<T | null>(null);

    const formHandler = useFormik<T>({
        initialValues,
        validationSchema,
        isInitialValid: false,
        onSubmit: async (values) => {
            if (formatter) values = formatter(values);

            const response = await mutation(url, method, request || values);

            if (setUser && response) setUser(response);
        },
    });

    return {
        formHandler,
        setRequest,
    }

}