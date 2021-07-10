import {useState} from 'react';
import {Method} from 'axios';

import {apiClient} from './ApiClient';
import {setApiMessage, toggleLoading, useAppDispatch} from '../../app/Store';

export interface ApiCaller {
    get: <T>(url: string, showSuccessMsg?: boolean) => Promise<T>;
    patch: <T1, T2>(url: string, data?: T2) => Promise<T1>;
    post: <T1, T2>(url: string, data?: T2) => Promise<T1>;
    put: <T1, T2>(url: string, data?: T2) => Promise<T1>;
    remove: <T>(url: string) => Promise<T>;
    setRequestInfo: (info: string) => void;
    requestInfo: string;
    working: boolean;
}

export function useApi(successMessage?: string): ApiCaller {
    const [working, setWorking] = useState(false);
    const [requestInfo, setRequestInfo] = useState('');

    const dispatch = useAppDispatch();

    const get = <T>(url: string, showSuccessMsg = false): Promise<T> => {
        return request<T>(url, 'GET', [], showSuccessMsg);
    };

    const patch = <T1, T2>(url: string, data: T2): Promise<T1> => {
        return request<T1>(url, 'PATCH', data);
    };

    const post = <T1, T2>(url: string, data: T2): Promise<T1> => {
        return request<T1>(url, 'POST', data);
    };

    const put = <T1, T2>(url: string, data: T2): Promise<T1> => {
        return request<T1>(url, 'PUT', data);
    };

    const remove = <T>(url: string): Promise<T> => {
        return request<T>(url, 'DELETE');
    };

    const request = async <T>(
        url: string,
        method: Method,
        request?: any,
        showSuccessMsg = true
    ): Promise<T> => {
        let response: T;

        console.log(url);

        try {
            dispatch(toggleLoading);
            setWorking(true);

            response = await apiClient.request({
                url,
                method,
                data: {
                    ...request,
                },
            });

            if (successMessage && showSuccessMsg) {
                setRequestInfo(successMessage);
                dispatch(setApiMessage(successMessage));
            }
        } catch (err) {
            console.log(err);
            dispatch(setApiMessage(err));
            setRequestInfo(err);
        } finally {
            dispatch(toggleLoading);
            setWorking(false);
        }

        return response!;
    };

    return {
        get,
        patch,
        post,
        put,
        remove,
        requestInfo,
        setRequestInfo,
        working,
    };
}

export const get = <T>(url: string, successMsg = ''): Promise<T> => {
    return useRequest<T>(url, 'GET', [], successMsg);
};

export const patch = <T1, T2>(url: string, data: T2): Promise<T1> => {
    return useRequest<T1>(url, 'PATCH', data);
};

export const post = <T1, T2>(url: string, data: T2, successMsg = ''): Promise<T1> => {
    return useRequest<T1>(url, 'POST', data, successMsg);
};

export const put = <T1, T2>(url: string, data: T2, successMsg = ''): Promise<T1> => {
    return useRequest<T1>(url, 'PUT', data, successMsg);
};

export const remove = <T>(url: string, successMsg = ''): Promise<T> => {
    return useRequest<T>(url, 'DELETE', {}, successMsg = '');
};

const useRequest = async <T>(
    url: string,
    method: Method,
    request?: any,
    successMessage?: string
): Promise<T> => {
    let response: T;
    const dispatch = useAppDispatch();

    console.log(url);

    try {
        response = await apiClient.request({
            url,
            method,
            data: {
                ...request,
            },
        });

        if (successMessage) {
            dispatch(setApiMessage(successMessage));
        }
    } catch (err) {
        console.log(err);
        dispatch(setApiMessage(err));
    } finally {
        dispatch(toggleLoading);
    }

    return response!;
};
