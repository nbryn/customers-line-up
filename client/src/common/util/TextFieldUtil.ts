import StringUtil from './StringUtil';
import {TextFieldType} from '../components/form/TextField';

function mapKeyToLabel(key: string, address = false): string {
   if (key === 'timeSlotLength') return 'Visit Length';

   if (address) return 'Address';
   return StringUtil.capitalize(key);
}

function mapKeyToType(key: string): TextFieldType {
   if (key === 'opens' || key === 'closes') return 'time';
   if (key === 'capacity' || key === 'timeSlotLength') return 'number';
   if (key === 'password') return 'password';
   if (key === 'email') return 'email';

   return 'text';
}

function shouldInputLabelShrink(key: string): boolean | undefined {
   if (key === 'opens' || key === 'closes') return true;

   return undefined;
}

export default {
   mapKeyToLabel,
   mapKeyToType,
   shouldInputLabelShrink,
};
