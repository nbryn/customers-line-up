import {DTO, HasAddress, MessageResponse} from '../../shared/models/General';
import {Address} from '../../shared/services/AddressService';

export interface BusinessDTO extends DTO, HasAddress {
    name: string;
    type: string;
    timeSlotLength: number | string;
    capacity: number | string;
    opens: string;
    closes: string;
    businessHours?: string;
    ownerEmail?: string;
}

export type BusinessDataDTO = {
    numberOfBookings: number;
    numberOfTimeSlots: number;
    numberOfEmployees?: number;
};

export interface BusinessMessageResponse extends MessageResponse {
    businessId: string;
}

export function updateBusiness(
    updatedBusiness: BusinessDTO,
    business: BusinessDTO,
    address: Address
) {
    updatedBusiness.longitude = address?.longitude ?? business.longitude;
    updatedBusiness.latitude = address?.latitude ?? business.latitude;
    updatedBusiness.city = address?.city ?? business.city;
    updatedBusiness.zip = address?.zip ?? business.zip;

    updatedBusiness.opens = updatedBusiness.opens.replace(':', '.');
    updatedBusiness.closes = updatedBusiness.closes.replace(':', '.');

    return updatedBusiness;
}
