import {useState} from 'react';
import {AxiosResponse, Method} from 'axios';

import {apiClient} from './ApiClient';

export interface ApiCaller {
    get: <T>(url: string) => Promise<T>;
    patch: <T>(url: string, data?: any) => Promise<T>;
    post: <T>(url: string, data?: any) => Promise<T>;
    put: <T>(url: string, data?: any) => Promise<T>;
    remove: <T>(url: string) => Promise<void>;
    setRequestInfo: (info: string) => void;
    requestInfo: string;
    working: boolean;
}

export function useApi(succesMessage?: string): ApiCaller {
    const [working, setWorking] = useState<boolean>(false);
    const [requestInfo, setRequestInfo] = useState<string>('');

    const get = async <T>(url: string): Promise<T> => {
        return await request<T>(url, 'GET');
    };

    const patch = async <T>(url: string, data: any): Promise<T> => {
        return await request<T>(url, 'PATCH', data);
    };

    const post = async <T>(url: string, data: any): Promise<T> => {
        return await request<T>(url, 'POST', data);
    };

    const put = async <T>(url: string, data: any): Promise<T> => {
        return await request<T>(url, 'PUT', data);
    };

    const remove = async <T>(url: string): Promise<T> => {
        return await request<T>(url, 'DELETE');
    };

    const request = async <T>(url: string, method: Method, request?: any): Promise<T> => {
        let response: T;

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

            if (succesMessage) setRequestInfo(succesMessage);
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
