import axios, { AxiosResponse, Method } from "axios";
import Cookies from 'js-cookie';

export async function fetch<T>(url: string, method: Method, request?: any): Promise<T> {
    let response: AxiosResponse<T>;
  
    setTokenInHeader();
  
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