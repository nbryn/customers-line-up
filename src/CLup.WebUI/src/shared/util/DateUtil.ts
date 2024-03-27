import moment from 'moment';

import type {ComboBoxOption} from '../components/form/ComboBox';

function getNext7Days(): ComboBoxOption[] {
    const days: ComboBoxOption[] = [];
    const daysRequired = 7;

    for (let i = 1; i <= daysRequired; i++) {
        const today = moment();
        const label = today.add(i, 'days').format('dddd, Do MMMM YYYY');

        days.push({label, date: today});
    }

    return days;
}

export default {
    getNext7Days,
};
