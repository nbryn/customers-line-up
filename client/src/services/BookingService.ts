import { BASE_URL } from './Url';
import ApiService from './ApiService';
import { TimeSlotDTO } from '../models/dto/Business';

// JWT will be used server side to get current user's email
async function fetchUserBookings(): Promise<TimeSlotDTO[]> {
    const bookings = await ApiService.fetch<TimeSlotDTO[]>(BASE_URL + 'booking/user', 'get');
 
    return bookings;
 }
 
 async function deleteBooking(timeSlotId: number): Promise<void> {
    const bookings = await ApiService.fetch<void>(BASE_URL + `booking/delete?timeSlotId=${timeSlotId}`, 'delete');
 
    return bookings;
 }

 async function createBooking(timeSlotId: number): Promise<void> {
    await ApiService.fetch<void>(BASE_URL + `booking/new?TimeSlotId=${timeSlotId}`, 'post');
}

 export default {
    fetchUserBookings,
    createBooking,
    deleteBooking,
 };