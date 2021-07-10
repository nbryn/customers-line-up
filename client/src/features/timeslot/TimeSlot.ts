import {DTO} from '../../common/models/General';

export interface TimeSlotDTO extends DTO {
    date: string;
    capacity: string;
    start: string;
    end: string;
}