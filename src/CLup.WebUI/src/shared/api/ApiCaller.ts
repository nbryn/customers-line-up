import type {Method} from 'axios';
import type {SerializedError} from '@reduxjs/toolkit';

import ApiClient from './ApiClient';

const get = <TResult>(url: string): Promise<TResult> => {
    return callApi<void, TResult>(url, 'GET');
};

const patch = <TData, TResult>(url: string, data: TData): Promise<TResult> => {
    return callApi<TData, TResult>(url, 'PATCH', data);
};

const post = <TData, TResult>(url: string, data?: TData): Promise<TResult> => {
    return callApi<TData, TResult>(url, 'POST', data);
};

const put = <TData>(url: string, data: TData): Promise<void> => {
    return callApi<TData, void>(url, 'PUT', data);
};

const remove = <TResult>(url: string): Promise<TResult> => {
    return callApi<void, TResult>(url, 'DELETE');
};

const callApi = async <TData, TResult>(url: string, method: Method, request?: TData): Promise<TResult> => {
    let response: TResult;

    console.log(url);

    try {
        response = await ApiClient.request({
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
