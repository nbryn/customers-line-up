import {BaseService} from '../../common/services/BaseService';
import {BookingDTO} from './Booking';
import {useApi} from '../../common/api/useApi';

const defaultRoute = 'booking';

export interface BookingService extends BaseService {
    createBooking: (timeSlotId: string) => Promise<void>;
    deleteBookingForBusiness: (timeSlotId: string, userEmail: string) => Promise<void>;
    deleteBookingForUser: (timeSlotId: string) => Promise<void>;
    fetchBookingsByBusiness: (businessId: string) => Promise<BookingDTO[]>;
    fetchBookingsByUser: () => Promise<BookingDTO[]>;
}

export function useBookingService(succesMessage?: string): BookingService {
    const apiCaller = useApi(succesMessage);

    const createBooking = (timeSlotId: string): Promise<void> => {
        return apiCaller.post(`${defaultRoute}/${timeSlotId}`);
    };

    const deleteBookingForBusiness = (timeSlotId: string, userEmail: string): Promise<void> => {
        return apiCaller.remove(`${defaultRoute}/business/${timeSlotId}?userEmail=${userEmail}`);
    };

    const deleteBookingForUser = (timeSlotId: string): Promise<void> => {
        return apiCaller.remove(`${defaultRoute}/user/${timeSlotId}`);
    };

    const fetchBookingsByBusiness = (businessId: string): Promise<BookingDTO[]> => {
        return apiCaller.get(`${defaultRoute}/business/${businessId}`);
    };

    const fetchBookingsByUser = (): Promise<BookingDTO[]> => {
        return apiCaller.get(`${defaultRoute}/user`);
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
