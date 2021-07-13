import {DTO} from '../../common/models/General';

export interface BusinessDTO extends DTO {
   address: string;
   name: string;
   zip: string;
   type: string;
   timeSlotLength: number | string;
   capacity: number | string;
   opens: string;
   closes: string;
   city?: string;
   businessHours?: string;
   ownerEmail?: string;
   longitude?: number;
   latitude?: number;
}

export type BusinessDataDTO = {
   numberOfBookings: number;
   numberOfTimeSlots: number;
   numberOfEmployees?: number;
};
