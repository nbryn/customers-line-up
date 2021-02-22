import axios, { AxiosResponse, Method } from "axios";

import {Error} from './Error';

export async function fetch<T>(url: string, method: Method, request?: any): Promise<T> {
    let response: AxiosResponse<T>;

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
        if (err.request.response._message) {
          throw new Error(err.request.response._message);
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