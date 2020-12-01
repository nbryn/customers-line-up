export interface BusinessDTO {
    id?: number;
    name: string;
    zip: number | string;
    type: string;
    opens: string;
    closes: string;  
}

export interface CreateBusinessDTO extends BusinessDTO {
    capacity: number | string;
}

export type TimeSlotDTO = {
    id: number;
    date: string;
    start: string;
    end: string;
    business?: string;
}