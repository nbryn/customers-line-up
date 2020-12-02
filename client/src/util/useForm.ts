import { Method } from "axios";
import { ObjectSchema } from 'yup';
import { useFormik, FormikComputedProps, FormikHandlers, FormikState } from 'formik';

import { RequestHandler } from '../api/RequestHandler';

export type Form<T> = FormikState<T> & FormikComputedProps<T> & FormikHandlers;

export const useForm = <T>(
    initialValues: T, 
    validationSchema: ObjectSchema,
    requestHandler: RequestHandler<void>,
    url: string,
    method: Method,
    formatter?: (dto: T) => T): Form<T> => {

    const formik = useFormik<T>({
        initialValues,
        validationSchema,
        onSubmit: async (values) => {
            if (formatter) values = formatter(values);

            await requestHandler.mutation(url, method, values);
        },
    });

  return formik;
}