import Cookies from 'js-cookie';

import {baseApi, USER_TAG} from '../../app/Store';
import {Configuration, UserApi} from '../../autogenerated';
import type {
    GetUserResponse,
    UpdateUserRequest,
    UsersNotEmployedByBusinessResponse,
} from '../../autogenerated';
import {apiMutation, apiQuery} from '../../shared/api/Api';

const USER_UPDATED_MSG = 'Info Updated.';

const userApiInstance = new UserApi(new Configuration({accessToken: Cookies.get('access_token')}));

const userApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        getUser: builder.query<GetUserResponse, void>({
            providesTags: [USER_TAG],
            queryFn: async (_, api) => ({
                data: await apiQuery(async (queryApi) => queryApi.getUser(), api),
            }),
        }),
        usersNotEmployedByBusiness: builder.query<UsersNotEmployedByBusinessResponse, string>({
            queryFn: async (businessId, api) => ({
                data: await apiQuery(
                    async (queryApi) =>
                        queryApi.getUsersNotAlreadyEmployedByBusiness(businessId, ''),
                    api
                ),
            }),
        }),
        updateUser: builder.mutation<void, UpdateUserRequest>({
            invalidatesTags: [USER_TAG],
            queryFn: async (updateUserRequest, api) => ({
                data: await apiMutation(
                    async () => userApiInstance.updateUser(updateUserRequest),
                    api,
                    {message: USER_UPDATED_MSG}
                ),
            }),
        }),
    }),
});

export const {useGetUserQuery} = userApi;
