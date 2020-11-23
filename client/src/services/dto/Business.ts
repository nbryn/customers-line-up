export interface BusinessDTO {
    name: string;
    zip: string;
    type: string;
    opens: string;
    closes: string;  
}

export interface CreateBusinessDTO extends BusinessDTO {
    capacity: string;
}