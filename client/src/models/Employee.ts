import {DTO} from './General';

export interface EmployeeDTO extends DTO {
    businessId?: number,
    name?: string,
    privateEmail?: string,
    companyEmail?: string,
    employedSince?: string,
}

export type NewEmployeeDTO = {
    businessId?: number,
    privateEmail?: string,
    companyEmail?: string,
}
