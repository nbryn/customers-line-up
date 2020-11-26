export interface BusinessDTO {
    id?: number;
    name: string;
    zip: number;
    type: string;
    opens: string;
    closes: string;  
}

export interface CreateBusinessDTO extends BusinessDTO {
    capacity: number;
}

export type TimeSlotDTO = {
    id: number;
    date: string;
    start: string;
    end: string;
    business?: string;
}