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
    content: string;
    senderId: string;
    sender: string;
    receiverId: string;
    receiver: string;
}

export interface MessageResponse {
    sentMessages: MessageDTO[];
    receivedMessages: MessageDTO[];
}

export interface SendMessage {
    title: string;
    content: string;
    type: string;
    receiverId: string;
    senderId?: string;
}
