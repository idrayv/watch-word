import { BaseResponseModel } from './models';
import { HttpClient } from "@angular/common/http";
let cfg = require('../config').appConfig;

export class BaseService {
    protected baseUrl: string = cfg.apiRoute;

    constructor(protected http: HttpClient){}

    protected getConnectionError<T extends BaseResponseModel>(): T {
        return <T>{
            errors: ['Server is unavailable. Please try again later.'],
            success: false
        };
    }
}