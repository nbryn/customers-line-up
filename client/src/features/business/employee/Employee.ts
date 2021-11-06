import {DTO} from '../../../shared/models/General';

export interface EmployeeDTO extends DTO {
    businessId?: string,
    userId?: string,
    name?: string,
    privateEmail?: string,
    companyEmail?: string,
    employedSince?: string,
}

export type NewEmployeeDTO = {
    businessId?: string,
    userId?: string,
    companyEmail?: string,
}
