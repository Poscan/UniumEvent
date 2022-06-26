export interface IResponse<TData, TError> {
    data?: TData;
    error?: TError;
    isSuccessful: boolean;
}