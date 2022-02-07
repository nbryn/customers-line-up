import {normalize, schema} from 'normalizr';
import { UserDTO } from '../features/user/User';
import { EntityState } from './EntitySlice';

const userAggregateSchema = (): schema.Entity => {
    const booking = new schema.Entity('bookings');
    const business = new schema.Entity('businesses');
    const employee = new schema.Entity('employees');
    const message = new schema.Entity('messages');
    const timeSlot = new schema.Entity('timeSlots');

    return new schema.Entity('users', {
        bookings: [booking],
        businesses: [business],
        employees: [employee],
        messages: [message],
        timeSlots: [timeSlot],
    });
};

export const normalizeUserAggregate = (user: UserDTO) => {
    const normalizedEntities = (normalize(user, userAggregateSchema()).entities as unknown) as EntityState
    
    return normalizedEntities;
}
