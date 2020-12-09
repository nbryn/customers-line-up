export interface DTO {
    id: number;
 }
 
export interface BusinessDTO extends DTO {
    name: string;
    zip: number | string;
    type: string;
    timeSlotLength: number | string;
    opens: string;
    closes: string;  
}

export interface CreateBusinessDTO extends BusinessDTO {
    capacity: number | string;
}

export interface TimeSlotDTO extends DTO {
    id: number;
    date: string;
    start: string;
    end: string;
    business?: string;
}