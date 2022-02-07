export interface NormalizedEntityState<T> {
    [id: string]: T;
}

export type ThunkParam<T1> = {
    id: string;
    data: T1;
};
