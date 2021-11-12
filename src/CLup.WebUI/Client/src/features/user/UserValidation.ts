import * as yup from 'yup';

export const emailSchemaObject: yup.ObjectSchema = yup.object({
   email: yup.string().email().required('Email is required'),
});

export const passwordValidationObject: yup.ObjectSchema = yup.object({
   password: yup
      .string()
      .min(4, 'Password must be at least 4 characters')
      .required('Password is required'),
});

export const userValidationSchema: yup.ObjectSchema = yup.object({
   email: yup.string().email().required('Email is required'),
   name: yup
      .string()
      .min(2, 'Name should be minimum 2 characters')
      .required('Name is required')
      .max(50, 'Maximum 50 characters'),
   zip: yup.string().required('Zip is required'),
   street: yup.string().min(4, 'Address should be 4 characters').required('Address is required'),
});

export const signupValidationSchema = userValidationSchema.concat(passwordValidationObject);

export const loginValidationSchema = emailSchemaObject.concat(passwordValidationObject);
