import type {DTO} from '../../../shared/models/General';

export interface TimeSlotDTO extends DTO {
    date: string;
    businessId: string;
    capacity: string;
    start: string;
    end: string;
    available: boolean;
}