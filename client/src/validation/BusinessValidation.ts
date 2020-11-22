import { validate } from 'validate.js';

import { ValidationResult } from './ValidationRunner';

const businessNameConstraints = {
    name: {
        presence: true,
        length: {
            minimum: 2,
            maximum: 30,
            message: 'must be between 2 and 30 characters',
        },
    },
};

function validateBusinessName(name: string): ValidationResult {
    const result: ValidationResult = validate({ name }, businessNameConstraints);

    return result;
}

export default {
    validateBusinessName,
}

