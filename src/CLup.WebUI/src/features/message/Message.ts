import type {DTO} from '../../shared/models/General';

export interface MessageDTO extends DTO {
    date: string;
    title: string;
    content: string;
    senderId: string;
    sender: string;
    receiverId: string;
    receiver: string;
    deletedBySender: boolean;
    deletedByReceiver: boolean;
}

export interface SendMessage {
    title: string;
    content: string;
    type: string;
    receiverId: string;
    senderId?: string;
}

export interface MarkMessageAsDeleted {
    messageId: string;
    forSender: boolean;
}

export interface MessageResponse {
    sentMessages: MessageDTO[];
    receivedMessages: MessageDTO[];
}

