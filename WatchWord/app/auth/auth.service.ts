import { Injectable } from '@angular/core';
import { Http, Response, Request, XHRBackend, RequestOptions, RequestOptionsArgs, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import { LoginModel, RegisterModel } from './auth.models';
import { BaseResponseModel } from '../abstract/models';
let cfg = require('../config').appConfig;

@Injectable()
export class AuthService {
    private baseUrl: string = cfg.apiRoute;

    constructor(private http: Http) { }

    authenticate(loginModel: LoginModel): Promise<BaseResponseModel> {
        return this.http.post(this.baseUrl + '/account/login', loginModel).toPromise()
            .then((res: Response) => res.json())
            .catch(() => { return { sucess: false, errors: ['Authentification error occured!'] } });
    }

    register(registerModel: RegisterModel): Promise<BaseResponseModel> {
        return this.http.post(this.baseUrl + '/account/register', registerModel).toPromise()
            .then((res: Response) => res.json())
            .catch(() => { return { sucess: false, errors: ['Registration error occured!'] } });
    }

    logout(): Promise<BaseResponseModel> {
        return this.http.post(this.baseUrl + '/account/logout', {}).toPromise()
            .then((res: Response) => res.json())
            .catch(() => { return { sucess: false, errors: ['Logout error occured!'] } });
    }
}

@Injectable()
export class AuthHttpService extends Http {
    constructor(backend: XHRBackend, defaultOptions: RequestOptions) {
        super(backend, defaultOptions);
    }

    request(url: string | Request, options?: RequestOptionsArgs): Observable<Response> {
        if (cfg.isDebug) {
            (<Request>url).withCredentials = true;
        }
        return super.request(url, options).catch((error: Response) => {
            if (error.status === 401 || error.status === 403) {
                console.log('The authentication session expires or the user is not authorised. Force refresh of the current page.');
                window.location.href = '/login';
            }
            return Observable.throw(error);
        });
    }
}