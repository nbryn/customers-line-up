import * as yup from 'yup';

export const signupValidationSchema: yup.ObjectSchema = yup.object({
    email: yup.string().email().required('Email is required'),
    name: yup.string().min(2, 'Name should be minimum 2 characters').required('Name is required'),
    zip: yup.string()
        .min(4, 'Zip should be 4 characters')
        .max(4, 'Zip should be 4 characters')
        .required('Zip is required'),
    address: yup.string()
        .min(4, 'Address should be 4 characters')
        .required('Address is required'),
    password: yup.string().min(4, 'Password must be at least 4 characters').required('Password is required'),
});

export const loginValidationSchema: yup.ObjectSchema = yup.object({
    email: yup.string().email().required('Email is required'),
    password: yup.string().min(4, 'Password must be at least 4 characters').required('Password is required'),
});