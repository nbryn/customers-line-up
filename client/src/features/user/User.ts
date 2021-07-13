export interface UserDTO {
    [key: string]: string | number | undefined;
    email: string;
    name?: string;
    zip?: string;
    address?: string;
    password?: string;
    token?: string;
    longitude?: number;
    latitude?: number;
    role?: 'User' | 'Employee' | 'Owner' | 'Admin';
}

export interface LoginDTO {
    [key: string]: string | number | undefined;
    email: string;
    password: string;
}

export type NotEmployedByBusiness = {
    businessId: string;
    users: UserDTO[];
};


