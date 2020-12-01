import axios, { AxiosResponse, Method } from "axios";
import Cookies from 'js-cookie';
import { useState } from 'react';

import { Error } from "./Error";

export interface RequestHandler<T, U> {
  mutation: (url: string, method: Method, request?: any) => Promise<U>;
  query: (url: string) => Promise<T>
  requestInfo: string;
  setRequestInfo: (info: string) => void;
}

export function useRequest<T, U>(): RequestHandler<T, U> {
  const [requestInfo, setRequestInfo] = useState<string>('');

  const mutation = async (url: string, method: Method, request?: any): Promise<U> => {
    let u: U;
    try {
      u = await fetch<U>(url, method, request);


    } catch (err) {
      console.log(err);
      setRequestInfo(err.getErrorMessage());
    }

    return u!;
  }


  const query = async (url: string): Promise<T> => {
    let t: T;
    try {
      t = await fetch<T>(url, 'GET',);


      return t;

    } catch (err) {
      setRequestInfo(err.getErrorMessage());
    }

    return t!;
  }

  return {
    query,
    mutation,
    requestInfo,
    setRequestInfo,
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