export type UserDTO = {
    [key: string]: string | boolean | undefined;
    email: string;
    name?: string;
    zip?: string;
    password?: string;
    token?: string;
    isOwner?: boolean;
};
