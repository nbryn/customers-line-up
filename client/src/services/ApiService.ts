import axios, { AxiosResponse, Method } from "axios";
import Cookies from 'js-cookie';

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

export default {
  fetch,
  request
};