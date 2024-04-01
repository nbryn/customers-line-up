import {MANAGE_EMPLOYEES_ROUTE} from '../../app/RouteConstants';
import {BUSINESS_BY_OWNER_TAG, BUSINESS_TAG, baseApi, USER_TAG} from '../../app/Store';
import {EmployeeApi, type CreateEmployeeRequest} from '../../autogenerated';
import {apiMutation} from '../../shared/api/Api';

const EMPLOYEE_CREATED_SUCCESS_INFO = {
    message: 'Employee Created - Go to my employees to see your employees',
    toastInfo: {
        buttonText: 'My Employees',
        navigateTo: MANAGE_EMPLOYEES_ROUTE,
    },
};

const employeeApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        createEmployee: builder.mutation<void, CreateEmployeeRequest>({
            invalidatesTags: [BUSINESS_TAG, BUSINESS_BY_OWNER_TAG, USER_TAG],
            queryFn: async (createEmployeeRequest, api) => ({
                data: await apiMutation(
                    async (employeeApi) => await employeeApi.createEmployee(createEmployeeRequest),
                    EmployeeApi,
                    api,
                    EMPLOYEE_CREATED_SUCCESS_INFO
                ),
            }),
        }),
        deleteEmployee: builder.mutation<void, string>({
            invalidatesTags: [BUSINESS_TAG, BUSINESS_BY_OWNER_TAG, USER_TAG],
            queryFn: async (employeeId, api) => ({
                data: await apiMutation(
                    async (employeeApi) => await employeeApi.deleteEmployee(employeeId),
                    EmployeeApi,
                    api
                ),
            }),
        }),
    }),
});

export const {useCreateEmployeeMutation, useDeleteEmployeeMutation} = employeeApi;
