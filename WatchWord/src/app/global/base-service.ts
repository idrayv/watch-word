import { HttpClient } from "@angular/common/http";

import { BaseResponseModel } from './models';
import { ServiceLocator } from './service-locator';
import { environment } from '../../environments/environment';

export class BaseService {
    protected http: HttpClient = ServiceLocator.Injector.get(HttpClient);
    protected baseUrl: string = environment.apiRoute;

    constructor() { }

    protected getConnectionError<T extends BaseResponseModel>(): T {
        return <T>{
            errors: ['Server is unavailable. Please try again later.'],
            success: false
        };
    }
}