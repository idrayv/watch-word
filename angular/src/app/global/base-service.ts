import {HttpClient} from '@angular/common/http';
import {BaseResponseModel} from './models';

export class BaseService {
    protected http: HttpClient;
    protected baseUrl: '';

    constructor() {
    }

    protected getConnectionError<T extends BaseResponseModel>(): T {
        return <T>{
            errors: ['Server is unavailable. Please try again later.'],
            success: false
        };
    }
}
