import {BaseService} from '../../common/services/BaseService';
import {TimeSlotDTO} from './TimeSlot';
import {useApi} from '../../common/api/useApi';

const defaultRoute = 'timeslot';

export interface TimeSlotService extends BaseService {
    deleteTimeSlot: (timeSlotId: string) => void;
    fetchAvailableTimeSlotsByBusiness: (businessId: string) => Promise<TimeSlotDTO[]>;
    fetchTimeSlotsByBusiness: (businessId: string) => Promise<TimeSlotDTO[]>;
    generateTimeSlots: (data: {businessId: string; start?: string}) => void;
}

export function useTimeSlotService(succesMessage?: string): TimeSlotService {
    const apiCaller = useApi(succesMessage);

    const deleteTimeSlot = (timeSlotId: string): void => {
        apiCaller.remove(`${defaultRoute}/${timeSlotId}`);
    };

    const fetchAvailableTimeSlotsByBusiness = (businessId: string): Promise<TimeSlotDTO[]> => {
        const today = new Date();
        today.setDate(today.getDate() - 100);
        const tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 30);

        const start = today.toISOString().substring(0, 10);
        const end = tomorrow.toISOString().substring(0, 10);

        return apiCaller.get(
            `${defaultRoute}/available?businessid=${businessId}&start=${start}&end=${end}`
        );
    };

    const fetchTimeSlotsByBusiness = (businessId: string): Promise<TimeSlotDTO[]> => {
        return apiCaller.get(`${defaultRoute}/business/${businessId}`);
    };

    const generateTimeSlots = (data: {businessId: string; start?: string}): void => {
        apiCaller.post(defaultRoute, data);
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
