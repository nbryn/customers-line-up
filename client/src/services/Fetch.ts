import axios, { AxiosResponse, Method } from "axios";
import Cookies from 'js-cookie';

import { Error } from "./Error";

export async function fetchFromServer<T>(url: string, method: Method, request?: any): Promise<T> {
  let response: AxiosResponse<T>;

  setTokenInHeader();

  console.log(url);

  // /* eslint-disable no-debugger */
  // debugger;

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
    console.log(err.response.data.errors);
    const errors = new Map();

    Object.keys(err.response.data).forEach((error) => {
      errors.set(error, err.response.data[error]);
    });

    throw new Error(errors);
  }

  return response.data;
}

export function setTokenInHeader(): void {
  if (Cookies.get('token')) {
    const token = Cookies.get('token');

    axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  }
}