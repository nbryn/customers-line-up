
export type ValidationResult = {
    [key: string]: string;
}

export type Props = {
    fieldId: string;
    input: string;
    setValue: (value: string) => any;
    setError: (error: string) => any;
    validate: (input: string) => ValidationResult;
}

function validateInput({ fieldId, input, setValue, setError, validate }: Props): void {
    setValue(input);

    const validation: ValidationResult = validate(input);

    if (validation) {
        setError(validation[fieldId]);
    } else {
        setError('');
    }
}

export default {
    validateInput
};