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

export type NotEmployedByBusiness = {
    businessId: string;
    users: UserDTO[];
};

export type UserInsights = {
    bookings: number;
    businesses: number;
    nextBookingBusiness: string;
    nextBookingTime: string;
};
