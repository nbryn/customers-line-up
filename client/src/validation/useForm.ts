import { Method } from "axios";
import { ObjectSchema } from 'yup';
import { useFormik, FormikComputedProps, FormikHandlers, FormikState } from 'formik';

export type Form<T> = FormikState<T> & FormikComputedProps<T> & FormikHandlers;

export const useForm = <T>(
    initialValues: T,
    validationSchema: ObjectSchema,
    url: string,
    method: Method,
    mutation: (url: string, method: Method, request: any) => Promise<T | null>,
    formatter?: (dto: T) => T,
    setUser?: (user: T) => void,
): Form<T> => {

    const form = useFormik<T>({
        initialValues,
        validationSchema,
        isInitialValid: false,
        onSubmit: async (values) => {
            if (formatter) values = formatter(values);

            const response = await mutation(url, method, values);

            if (setUser && response) setUser(response!)
        },
    });

    return form;
    
}