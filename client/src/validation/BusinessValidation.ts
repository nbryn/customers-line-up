import * as yup from 'yup';

export const generalCreateBusinessErrorMsg = 'An error occured';

export const createBusinessValidationSchema: yup.ObjectSchema = yup.object({
    name: yup.string().min(2, 'Name should be minimum 2 characters').required('Name is required'),
    zip: yup.string()
        .min(4, 'Zip should be 4 characters')
        .max(4, 'Zip should be 4 characters')
        .required('Zip is required'),
    type: yup.string().required('Type is required'),
    capacity: yup.number().min(1, 'Capacity should be over 0').required('Capacity is required'),
    timeSlotLength: yup.number().min(1, 'Visit length should be over 0').required('Visit length is required'),
    opens: yup.string().required('Opens is required'),
    closes: yup.string().required('Closes is required'),
});
