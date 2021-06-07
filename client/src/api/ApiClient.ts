import axios, {AxiosRequestConfig} from 'axios';
import Cookies from 'js-cookie';

export const apiClient = axios.create({
    baseURL: process.env.REACT_APP_API_URI,
    timeout: 1800000,
});

apiClient.interceptors.request.use((request: AxiosRequestConfig) => {
    const accessToken = Cookies.get('access_token');

    if (!accessToken) {
        return request;
    }

    request.headers['Authorization'] = `Bearer ${accessToken}`;
    return request;
});

apiClient.interceptors.response.use(
    (response) => response.data,
    async (error: any) => {
        const response = error.response;

        console.log(response);

        const refreshToken = Cookies.get('refresh_token');

        if (response) {
            if (response.status === 401 && error.config && !error.config._retry) {
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

            if (response.data) {
                console.log(response.data);
                return Promise.reject(response.data);
            }
        }

        return Promise.reject(error);
    }
);
