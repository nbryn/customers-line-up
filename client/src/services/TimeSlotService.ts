import { BASE_URL } from './Url';
import ApiService from './ApiService';
import { TimeSlotDTO } from '../models/dto/Business';

async function fetchAvailableTimeSlotsForBusiness(businessId: number): Promise<TimeSlotDTO[]> {
    const today = new Date();
    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 3);

    const start = today.toISOString().substring(0, 10);
    const end = tomorrow.toISOString().substring(0, 10);

    const queues: TimeSlotDTO[] = await ApiService.fetch<TimeSlotDTO[]>(BASE_URL + `timeslot/available?businessid=${businessId}&start=${start}&end=${end}`, 'get');

    return queues;
}

export default {
    fetchAvailableTimeSlotsForBusiness,
};