export const BASE_URL = process.env.REACT_APP_API_URI;

const getTimeSlotURL = (businessId: number): string => {
    const today = new Date();
    today.setDate(today.getDate() - 100);
    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 3);

    const start = today.toISOString().substring(0, 10);
    const end = tomorrow.toISOString().substring(0, 10);

    return BASE_URL + `timeslot/available?businessid=${businessId}&start=${start}&end=${end}`;
}

const getCreateBookingURL = (timeSlotId: number): string => {
    return BASE_URL + `booking/new?timeSlotId=${timeSlotId}`;
}

const getDeleteBookingURL = (timeSlotId: number): string => {
    return BASE_URL + `booking/delete?timeSlotId=${timeSlotId}`;
}

const getBusinessBookingsURL = (businessId: number): string => {
    return BASE_URL + `booking/business?businessId=${businessId}`;
}

// Server will get user info from JWT
export const LOGIN_URL = BASE_URL + 'user/login';

export const ALL_BUSINESSES_URL = BASE_URL + 'business/all';

export const BUSINESSES_OWNER_URL = BASE_URL + 'business/owner';

export const BUSINESS_TYPES = BASE_URL + 'business/types';

export const CREATE_BUSINESS_URL = BASE_URL + 'business/create';

export const USER_BOOKINGS_URL = BASE_URL + 'booking/user';


export default {
    getBusinessBookingsURL,
    getCreateBookingURL,
    getDeleteBookingURL,
    getTimeSlotURL,
};