import axios, { AxiosResponse, Method } from "axios";
import Cookies from 'js-cookie';
import React, { useState } from 'react';

import { Error } from "./Error";

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
    if (err.reponse) {
      console.log(err.response.data);
      throw new Error(err.response.data.message);

    } else if (err.request) {
      console.log(err.request);
      throw new Error("Network Error - Please try again");

    } else {
      throw new Error("Undefined Error!")
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

export async function request<T>(query: () => T, showErrorMsg: (errorMsg: string) => void): Promise<T> {
  let t: T;
  try {

    t = await query();

  } catch (err) {
    showErrorMsg(err.getErrorMessage());
  }

  return t!;
}

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

export default {
  fetch,
  request
};