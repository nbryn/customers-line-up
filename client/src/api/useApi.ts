import {useState} from 'react';
import {Method} from 'axios';

import {apiClient} from './ApiClient';

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
    const [working, setWorking] = useState<boolean>(false);
    const [requestInfo, setRequestInfo] = useState<string>('');

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

    const request = async <T>(url: string, method: Method, request?: any, showSuccessMsg = true): Promise<T> => {
        let response: T;

        console.log(showSuccessMsg);

        console.log(url);

        try {
            setWorking(true);

            response = await apiClient.request({
                url,
                method,
                data: {
                    ...request,
                },
            });

            if (successMessage && showSuccessMsg) setRequestInfo(successMessage);
        } catch (err) {
            console.log(err);
            setRequestInfo(err);
        } finally {
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
