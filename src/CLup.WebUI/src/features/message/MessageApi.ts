import Cookies from 'js-cookie';

import {BUSINESS_BY_OWNER_TAG, BUSINESS_TAG, baseApi, USER_TAG} from '../../app/Store';
import {
    Configuration,
    MessageApi,
    type SendUserMessageRequest,
    type SendBusinessMessageRequest,
    type MarkMessageAsDeletedForUserRequest,
    type MarkMessageAsDeletedForBusinessRequest,
} from '../../autogenerated';

const messageApiInstance = new MessageApi(
    new Configuration({accessToken: Cookies.get('access_token')})
);

const messageApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        sendUserMessage: builder.mutation<void, SendUserMessageRequest>({
            invalidatesTags: [BUSINESS_TAG, BUSINESS_BY_OWNER_TAG, USER_TAG],
            queryFn: async (sendUserMessageRequest) => {
                try {
                    await messageApiInstance.sendUserMessage(sendUserMessageRequest);
                } catch (error) {
                    return {error} as any;
                }
            },
        }),
        sendBusinessMessage: builder.mutation<void, SendBusinessMessageRequest>({
            invalidatesTags: [BUSINESS_TAG, BUSINESS_BY_OWNER_TAG, USER_TAG],
            queryFn: async (sendBusinessMessageRequest) => {
                try {
                    await messageApiInstance.sendBusinessMessage(sendBusinessMessageRequest);
                } catch (error) {
                    return {error} as any;
                }
            },
        }),
        markMessageAsDeletedForUser: builder.mutation<void, MarkMessageAsDeletedForUserRequest>({
            invalidatesTags: [BUSINESS_TAG, BUSINESS_BY_OWNER_TAG, USER_TAG],
            queryFn: async (markMessageAsDeletedForUserRequest) => {
                try {
                    await messageApiInstance.markMessageAsDeletedForUser(
                        markMessageAsDeletedForUserRequest
                    );
                } catch (error) {
                    return {error} as any;
                }
            },
        }),
        markMessageAsDeletedForBusiness: builder.mutation<
            void,
            MarkMessageAsDeletedForBusinessRequest
        >({
            invalidatesTags: [BUSINESS_TAG, BUSINESS_BY_OWNER_TAG, USER_TAG],
            queryFn: async (markMessageAsDeletedForBusinessRequest) => {
                try {
                    await messageApiInstance.markMessageAsDeletedForBusiness(
                        markMessageAsDeletedForBusinessRequest
                    );
                } catch (error) {
                    return {error} as any;
                }
            },
        }),
    }),
});

export const {
    useSendUserMessageMutation,
    useSendBusinessMessageMutation,
    useMarkMessageAsDeletedForUserMutation,
    useMarkMessageAsDeletedForBusinessMutation,
} = messageApi;
