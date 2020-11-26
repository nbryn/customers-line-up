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

export type BusinessQueueDTO = {
    id: number;
    date: string;
    start: string;
    end: string;
    business?: string;
}