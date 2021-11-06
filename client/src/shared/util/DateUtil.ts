import moment from 'moment';

import {ComboBoxOption} from '../components/form/ComboBox';

function getNext7Days(): ComboBoxOption[] {
   const days: ComboBoxOption[] = [];
   const daysRequired = 7;

   for (let i = 1; i <= daysRequired; i++) {
      const today = moment();
      const label = today.add(i, 'days').format('dddd, Do MMMM YYYY');
      const value = today.format(moment.HTML5_FMT.DATE);

      days.push({label, value});
   }

   return days;
}

export default {
   getNext7Days,
};
