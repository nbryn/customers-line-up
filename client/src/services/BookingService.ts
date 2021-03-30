import {BaseService} from './BaseService';
import {BookingDTO} from '../models/Booking';
import {useApi} from '../api/useApi';

const defaultRoute = 'booking';

export interface BookingService extends BaseService {
    createBooking: (timeSlotId: number) => Promise<void>;
    deleteBookingForBusiness: (timeSlotId: number, userEmail: string) => Promise<void>;
    deleteBookingForUser: (timeSlotId: number) => Promise<void>;
    fetchBookingsByBusiness: (businessId: number) => Promise<BookingDTO[]>;
    fetchBookingsByUser: () => Promise<BookingDTO[]>;
}

export function useBookingService(succesMessage?: string): BookingService {
    const apiCaller = useApi(succesMessage);

    const createBooking = async (timeSlotId: number): Promise<void> => {
        await apiCaller.post(`${defaultRoute}/${timeSlotId}`);
    };

    const deleteBookingForBusiness = async (
        timeSlotId: number,
        userEmail: string
    ): Promise<void> => {
        await apiCaller.remove(`${defaultRoute}/business/${timeSlotId}?userEmail=${userEmail}`);
    };

    const deleteBookingForUser = async (timeSlotId: number): Promise<void> => {
        await apiCaller.remove(`${defaultRoute}/user${timeSlotId}`);
    };

    const fetchBookingsByBusiness = async (businessId: number): Promise<BookingDTO[]> => {
        return await apiCaller.get(`${defaultRoute}/business/${businessId}`);
    };

    const fetchBookingsByUser = async (): Promise<BookingDTO[]> => {
        return await apiCaller.get(`${defaultRoute}/user`);
    };

    const setRequestInfo = (info: string) => apiCaller.setRequestInfo(info);

    return {
        createBooking,
        deleteBookingForBusiness,
        deleteBookingForUser,
        fetchBookingsByBusiness,
        fetchBookingsByUser,
        setRequestInfo,
        requestInfo: apiCaller.requestInfo,
        working: apiCaller.working,
    };
}
