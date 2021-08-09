import {State} from '../../app/AppTypes';

export interface ApiInfo {
    isError: boolean;
    isLoading: boolean;
    message: string;
    state: State;
    buttonText?: string;
}

export type Args = Omit<ApiInfo, "isError" | "isLoading">;

export const defaultApiInfo = (state: State): ApiInfo => ({
    isError: false,
    isLoading: false,
    message: '',
    state,
});

export const apiError = ({state, message, buttonText}: Args): ApiInfo => ({
    isError: true,
    isLoading: false,
    message,
    state,
    buttonText
});

export const apiSuccess = ({state, message, buttonText}: Args): ApiInfo => ({
    isError: false,
    isLoading: false,
    message,
    state,
    buttonText
});
