import * as yup from 'yup';

export const emailSchemaObject: yup.ObjectSchema = yup.object({
    email: yup.string().email('Must be a valid email').required('Email is required'),
});

export const passwordValidationObject: yup.ObjectSchema = yup.object({
    password: yup
        .string()
        .min(4, 'Password must be at least 4 characters')
        .required('Password is required'),
});

export const addressValidationSchema: yup.ObjectSchema = yup.object({
    zip: yup.string().required('Zip is required'),
    street: yup
        .string()
        .min(4, 'Street should be at least 4 characters')
        .required('Address is required'),
});

export const userValidationSchema: yup.ObjectSchema = yup.object({
    name: yup
        .string()
        .min(2, 'Name should be minimum 2 characters')
        .required('Name is required')
        .max(50, 'Maximum 50 characters'),
});

export const registerValidationSchema = emailSchemaObject
    .concat(userValidationSchema)
    .concat(addressValidationSchema.concat(passwordValidationObject));

export const loginValidationSchema = emailSchemaObject.concat(passwordValidationObject);
