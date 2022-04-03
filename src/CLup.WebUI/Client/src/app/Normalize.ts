import {normalize, schema, Schema} from 'normalizr';
import {BusinessDTO} from '../features/business/Business';
import {UserDTO} from '../features/user/User';
import {EntityState} from './EntitySlice';

const bookingSchema = new schema.Entity('bookings');
const employeeSchema = new schema.Entity('employees');
const messageSchema = new schema.Entity('messages');
const timeSlotSchema = new schema.Entity('timeSlots');

const businessSchema = (): schema.Entity =>
    new schema.Entity('businesses', {
        bookings: [bookingSchema],
        employees: [employeeSchema],
        messages: [messageSchema],
        timeSlots: [timeSlotSchema],
    });

const userAggregateSchema = (): Schema => {
    const businessSchema = new schema.Entity('businesses', {
        bookings: [bookingSchema],
        employees: [employeeSchema],
        messages: [messageSchema],
        timeSlots: [timeSlotSchema],
    });

    const userSchema = new schema.Entity('user', {
        bookings: [bookingSchema],
        businesses: [businessSchema],
        messages: [messageSchema],
    });

    return {user: userSchema};
};

export const normalizeUserAggregate = (user: UserDTO) => {
    const normalizedEntities = (normalize({user}, userAggregateSchema())
        .entities as unknown) as EntityState;

    return normalizedEntities;
};

export const normalizeBusinesses = (businesses: BusinessDTO[]) => {
    const normalizedEntities = normalize(businesses, [businessSchema()]);

    if (!normalizedEntities.entities.messages) {
        normalizedEntities.entities.messages = {'': ''};
    }

    if (!normalizedEntities.entities.employees) {
        normalizedEntities.entities.employees = {'': ''};
    }

    return (normalizedEntities.entities as unknown) as EntityState;
};
