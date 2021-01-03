export interface UserDTO {
    [key: string]: string | number | undefined;
    email: string;
    name?: string;
    zip?: string;
    address?: string;
    password?: string;
    token?: string;
    role?: 'User' | 'Employee' | 'Owner' | 'Admin';
}
