export interface NormalizedEntityState<T> {
    byId: {[id: string]: T};
    allIds: string[];
}

export type ThunkParam<T1> = {
    id: string;
    data: T1;
};
