import {DTO} from '../../common/models/General';

export interface EmployeeDTO extends DTO {
    businessId?: string,
    name?: string,
    privateEmail?: string,
    companyEmail?: string,
    employedSince?: string,
}

export type NewEmployeeDTO = {
    businessId?: string,
    privateEmail?: string,
    companyEmail?: string,
}
