import {Method} from 'axios';
import {SerializedError} from '@reduxjs/toolkit';

import {apiClient} from './ApiClient';

const get = <T>(url: string): Promise<T> => {
    return callApi<T>(url, 'GET');
};

const patch = <T1, T2>(url: string, data: T2): Promise<T1> => {
    return callApi<T1>(url, 'PATCH', data);
};

const post = <T1, T2>(url: string, data?: T2): Promise<T1> => {
    return callApi<T1>(url, 'POST', data);
};

const put = <T1, T2>(url: string, data: T2): Promise<T1> => {
    return callApi<T1>(url, 'PUT', data);
};

const remove = <T>(url: string): Promise<T> => {
    return callApi<T>(url, 'DELETE');
};

const callApi = async <T>(url: string, method: Method, request?: any): Promise<T> => {
    let response: T;

    console.log(url);

    try {
        response = await apiClient.request({
            url,
            method,
            data: {
                ...request,
            },
        });
    } catch (err) {
        console.log(err);
        throw {message: err} as SerializedError;
    }

    return response!;
};

export default {
    get,
    patch,
    post,
    put,
    remove,
};
