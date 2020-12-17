import { TextFieldType } from '../components/TextFieldModal';

function unCapitalizeFirstLetter(string: string): string {
    return string.charAt(0).toLowerCase() + string.slice(1);
}

function capitalizeFirstLetter(string: string): string {
    return string.charAt(0).toUpperCase() + string.slice(1);
}

function mapDTOKeyToLabel(key: string): string {
    if (key === 'businessHours') return 'Business Hours';
    if (key === 'timeSlotLength') return 'Visit Length';

    return capitalizeFirstLetter(key);
}

function mapLabelToDTOKey(key: string): string {
    if (key === 'Business Hours') return 'businessHours';
    if (key === 'Visit Length') return 'timeSlotLength';

    return unCapitalizeFirstLetter(key);
}

function getTextFieldTypeFromKey(key: string): TextFieldType {
    if (key === 'businessHours') return 'time';
    if (key === 'capacity' || key === 'timeSlotLength') return 'number';


    return 'text';
}

export default {
    capitalizeFirstLetter,
    getTextFieldTypeFromKey,
    mapLabelToDTOKey,
    mapDTOKeyToLabel,
}