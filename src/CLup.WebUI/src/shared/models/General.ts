export interface Index {
    [key: string]: DTO | DTO[] | string | number | boolean | undefined;
}

export interface DTO extends Index {
    id: string;
}

export interface HasAddress extends Index {
    street: string;
    zip: string;
    city?: string;
    longitude?: number;
    latitude?: number;
}
