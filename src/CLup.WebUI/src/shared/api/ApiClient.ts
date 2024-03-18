import type {InternalAxiosRequestConfig} from 'axios';
import axios from 'axios';
import Cookies from 'js-cookie';

const apiClient = axios.create({
    baseURL: process.env.REACT_APP_API_URI,
    timeout: 1800000,
});

apiClient.interceptors.request.use((request: InternalAxiosRequestConfig) => {
    const accessToken = Cookies.get('access_token');

    if (!accessToken) {
        return request;
    }

    request.headers['Authorization'] = `Bearer ${accessToken}`;
    return request;
});

apiClient.interceptors.response.use(
    (response) => response.data,
    async (e: any) => {
        const error = e.response;

        console.log(error);

        const refreshToken = Cookies.get('refresh_token');

        if (error) {
            if (error.status === 401 && error.config && !error.config._retry) {
                try {
                    if (refreshToken) {
                        /* const newRefreshToken = Cookies.get('refresh_token');
            const accessToken = Cookies.get('access_token'); */

                        /* const model = {
              accessToken,
              refreshToken: newRefreshToken,
            } as refreshTokenModel;
  
            const authManager = new authService();
            await authManager.refreshToken(model); */
                        return await apiClient.request(error.config);
                    }
                } catch (authError) {
                    return Promise.reject(error);
                }
                error.config._retry = true;
                return Promise.reject(error);
            }

            if (error.data) {
                console.log(error.data);
                return Promise.reject(error.data);
            }
        }

        return Promise.reject(error);
    }
);

export default apiClient;