import { Method } from "axios";
import { ObjectSchema } from 'yup';
import { useFormik, FormikComputedProps, FormikHandlers, FormikState } from 'formik';

export type Form<T> = FormikState<T> & FormikComputedProps<T> & FormikHandlers;

export const useForm = <T>(
    initialValues: T, 
    validationSchema: ObjectSchema,
    mutation: (url: string, method: Method, request: any) => Promise<void>,
    url: string,
    method: Method,
    formatter?: (dto: T) => T): Form<T> => {

    const form = useFormik<T>({
        initialValues,
        validationSchema,
        isInitialValid: false,
        onSubmit: async (values) => {
            if (formatter) values = formatter(values);

            await mutation(url, method, values);
        },
    });

  return form;
}