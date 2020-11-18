import axios, { AxiosResponse, Method } from "axios";

import { Error } from "./Error";

export async function fetchFromServer<T>(url: string, method: Method, request?: any): Promise<T> {
  let response: AxiosResponse<T>;

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
    const errors = new Map();

    Object.keys(err.response.data).forEach((error) => {
      errors.set(error, err.response.data[error]);
    });

    throw new Error(errors);
  }

  return response.data;
}

export function setTokenInHeader(token: string): void {
  axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
}