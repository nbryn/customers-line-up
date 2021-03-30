import {BaseService} from './BaseService';
import {TimeSlotDTO} from '../models/TimeSlot';
import {useApi} from '../api/useApi';

const defaultRoute = 'timeslot';

export interface TimeSlotService extends BaseService {
    deleteTimeSlot: (timeSlotId: number) => Promise<void>;
    fetchAvailableTimeSlotsByBusiness: (businessId: number) => Promise<TimeSlotDTO[]>;
    fetchTimeSlotsByBusiness: (businessId: number) => Promise<TimeSlotDTO[]>;
    generateTimeSlots: (data: {businessId: number, start?: string}) => Promise<void>;
}

export function useTimeSlotService(succesMessage?: string): TimeSlotService {
    const apiCaller = useApi(succesMessage);

    const deleteTimeSlot = async (timeSlotId: number): Promise<void> => {
        await apiCaller.remove(`${defaultRoute}/${timeSlotId}`);
    };

    const fetchAvailableTimeSlotsByBusiness = async (
        businessId: number
    ): Promise<TimeSlotDTO[]> => {
        const today = new Date();
        today.setDate(today.getDate() - 100);
        const tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 30);

        const start = today.toISOString().substring(0, 10);
        const end = tomorrow.toISOString().substring(0, 10);

        return await apiCaller.get(
            `${defaultRoute}/available?businessid=${businessId}&start=${start}&end=${end}`
        );
    };

    const fetchTimeSlotsByBusiness = async (businessId: number): Promise<TimeSlotDTO[]> => {
        return await apiCaller.get(`${defaultRoute}/business/${businessId}`);
    };

    const generateTimeSlots = async (data: {businessId: number, start?: string}): Promise<void> => {
        await apiCaller.post(defaultRoute, data);
    };

    const setRequestInfo = (info: string) => apiCaller.setRequestInfo(info);

    return {
        deleteTimeSlot,
        fetchAvailableTimeSlotsByBusiness,
        fetchTimeSlotsByBusiness,
        generateTimeSlots,
        setRequestInfo,
        requestInfo: apiCaller.requestInfo,
        working: apiCaller.working,
    };
}
