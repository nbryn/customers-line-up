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

export interface MessageDTO extends DTO {
    date: string;
    title: string;
    content: string
}

export interface ReceivedMessageDTO extends MessageDTO {
    senderId: string;
    sender: string;
}

export interface SentMessageDTO extends MessageDTO {
    receiverId: string;
    receiver: string;
}
 