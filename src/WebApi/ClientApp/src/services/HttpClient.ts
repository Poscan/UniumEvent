import { IResponse } from "@/services/models/Response";
import axios, { AxiosRequestConfig } from "axios";

interface IHttpClient {
    get<T, E>(url: string, config?: AxiosRequestConfig): Promise<IResponse<T, E>>;
    post<T, E>(url: string, data?: unknown, config?: AxiosRequestConfig): Promise<IResponse<T, E>>;
    delete<T, E>(url: string, config?: AxiosRequestConfig): Promise<IResponse<T, E>>;
}

class HttpClient implements IHttpClient {
    public async get<T, E>(url: string, config?: AxiosRequestConfig): Promise<IResponse<T, E>> {
        try {
            const response = await axios.get<IResponse<T, E>>(url, config);
            return response.data;
        } catch (err) {
            console.error(err);
            return { isSuccessful: false } as IResponse<T, E>;
        }
    }

    public async post<T, E>(url: string, data?: unknown, config?: AxiosRequestConfig): Promise<IResponse<T, E>> {
        try {
            const response = await axios.post<IResponse<T, E>>(url, data, config);
            return response.data;
        } catch (err) {
            console.error(err);
            return { isSuccessful: false } as IResponse<T, E>;
        }
    }

    public async delete<T, E>(url: string, config?: AxiosRequestConfig): Promise<IResponse<T, E>> {
        try {
            const response = await axios.delete<IResponse<T, E>>(url, config);

            return response.data;
        } catch (err) {
            console.error(err);
            return { isSuccessful: false } as IResponse<T, E>;
        }
    }
}

const httpClient: IHttpClient = new HttpClient();

export default httpClient;