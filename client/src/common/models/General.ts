export interface Index {
    [key: string]: string | number | undefined;
}
export interface DTO extends Index {
    [key: string]: string | number | undefined;
    id: string;
 }

 export interface HasAddress extends Index {
    street: string;
    zip: string;
    city?: string;
    longitude?: number;
    latitude?: number;
}
 