export interface BaseService {
    requestInfo: string,
    working: boolean,
    setRequestInfo: (info: string) => void;
}
