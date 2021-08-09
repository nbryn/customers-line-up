import {ApiInfo} from '../common/api/ApiInfo';

export enum State {
    Bookings = 'Bookings',
    Businesses = 'Businesses',
    Employees = 'Employees',
    Insights = 'Insights',
    TimeSlots = 'timeSlots',
    Users = 'users',
}

export interface NormalizedEntityState<T> {
    byId: {[id: string]: T};
    allIds: string[];
    apiInfo: ApiInfo;
}

export type ThunkParam<T1> = {
    id: string;
    data: T1;
};
