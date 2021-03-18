import axios, { Method } from "axios";
import Cookies from 'js-cookie';
import { useState } from 'react';

import {fetch} from '../api/Fetch';

export interface ApiCaller<T> {
  mutation: <T>(url: string, method: Method, request?: any) => Promise<T>;
  query: (url: string) => Promise<T>
  setRequestInfo: (info: string) => void;
  requestInfo: string;
  working: boolean;
}

export function useApi<T>(succesMessage?: string): ApiCaller<T> {
  const [working, setWorking] = useState<boolean>(false);
  const [requestInfo, setRequestInfo] = useState<string>('');

  setTokenInHeader();

  const mutation = async <T>(url: string, method: Method, request?: any): Promise<T> => {
    let response: T;
    try {
      setWorking(true);
      response = await fetch(url, method, request);

      if (succesMessage) setRequestInfo(succesMessage);

    } catch (err) {
      console.log(err);
      setRequestInfo(err.getErrorMessage());
    } finally {
      setWorking(false);
    }

    return response!;
  }

  const query = async (url: string): Promise<T> => {
    let response: T;
    try {
      setWorking(true);
      response = await fetch<T>(url, 'GET');

    } catch (err) {
      setRequestInfo(err.getErrorMessage());
    } finally {
      setWorking(false);
    }

    return response!;
  }


  return {
    query,
    mutation,
    requestInfo,
    setRequestInfo,
    working,
  }
}


function setTokenInHeader(): void {
  if (Cookies.get('token')) {
    const token = Cookies.get('token');

    axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  }
}


