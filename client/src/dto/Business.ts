export interface DTO {
    id: number;
 }
 
export interface BusinessDTO extends DTO {
    [key: string]: string | number | undefined;
    name: string;
    zip: number | string;
    type: string;
    timeSlotLength: number | string;  
    capacity: number | string;  
    opens: string;
    closes: string;
}

export type BusinessDataDTO = {
    numberOfBookings: number;
    numberOfTimeSlots: number;
    numberOfEmployees?: number;
}

export interface TimeSlotDTO extends DTO {
    id: number;
    date: string;
    start: string;
    end: string;
    business?: string;
}