import {DTO} from './General';

export interface EmployeeDTO extends DTO {
    businessId: number,
    name: string,
    privateEmail: string,
    companyEmail: string,
    employedSince: string,
}