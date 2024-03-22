import type {ExtendedAddress} from '../services/AddressService';
import type {HasAddress} from '../models/General';
import type {Index} from '../models/General';
import StringUtil from './StringUtil';
import type {TextFieldType} from '../components/form/TextField';

function mapKeyToLabel(key: string, address = false): string {
    if (key === 'timeSlotLength') return 'Visit Length';

    if (address) return 'Street';
    return StringUtil.capitalize(key);
}

function mapKeyToStep(key: string): number {
    if (key === 'opens' || key === 'closes') return 1800;

    return 1;
}

function mapKeyToType(key: string): TextFieldType {
    if (key === 'opens' || key === 'closes') return 'time';
    if (key === 'capacity' || key === 'timeSlotLength') return 'number';
    if (key === 'password') return 'password';
    if (key === 'email') return 'email';

    return 'text';
}

function mapKeyToValue(
    key: string,
    values: Index,
    addresses: ExtendedAddress[],
    entity: HasAddress
): string {
    if (key === 'zip') {
        if (!addresses.length) return `${entity.zip} - ${entity.city}`;
        const address = addresses.find((a) => a.zip === values[key]);
        return `${values[key]} - ${address?.city ?? ''}`;
    }

    if (key === 'opens' || key === 'closes') {
        return values[key]?.toString().replace('.', ':') as string;
    }

    return values[key] as any;
}

function shouldInputLabelShrink(key: string): boolean | undefined {
    if (key === 'opens' || key === 'closes') return true;

    return undefined;
}

export default {
    mapKeyToLabel,
    mapKeyToStep,
    mapKeyToType,
    mapKeyToValue,
    shouldInputLabelShrink,
};
