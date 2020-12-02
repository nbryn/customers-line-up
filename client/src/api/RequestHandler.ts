import axios, { AxiosResponse, Method } from "axios";
import Cookies from 'js-cookie';
import { useState } from 'react';

import { Error } from "./Error";

export interface RequestHandler<T> {
  mutation: (url: string, method: Method, request?: any) => Promise<void>;
  query: (url: string) => Promise<T>
  requestInfo: string;
  setRequestInfo: (info: string) => void;
  working: boolean;
}

export function useRequest<T>(succesMessage?: string): RequestHandler<T> {
  const [working, setWorking] = useState<boolean>(false);
  const [requestInfo, setRequestInfo] = useState<string>('');

  const mutation = async (url: string, method: Method, request?: any): Promise<void> => {
    try {
      setWorking(true);
      await fetch(url, method, request);
      if (succesMessage) setRequestInfo(succesMessage);

    } catch (err) {
      console.log(err);
      setRequestInfo(err.getErrorMessage());
    } finally {
      setWorking(false);
    }
  }

  const query = async (url: string): Promise<T> => {
    let response: T;
    try {
      setWorking(true);
      response = await fetch<T>(url, 'GET',);

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

export async function fetch<T>(url: string, method: Method, request?: any): Promise<T> {
  let response: AxiosResponse<T>;

  setTokenInHeader();

  console.log(url);

  try {
    response = await axios({
      url,
      method,
      data: {
        ...request
      },
    });

  } catch (err) {
    console.log(err);
    if (err.request.response) {
      console.log(err.request.response);
      if (err.request.response.message) {
        throw new Error(err.request.response.message);
      }

      throw new Error(err.request.response);
    } else {
      throw new Error("Network/Undefined Error!")
    }
    // const errors = new Map();

    // Object.keys(err.response.data).forEach((error) => {
    //   errors.set(error, err.response.data[error]);
    // });

  }

  return response.data;
}

export function setTokenInHeader(): void {
  if (Cookies.get('token')) {
    const token = Cookies.get('token');

    axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  }
}

export default {
  fetch,
};