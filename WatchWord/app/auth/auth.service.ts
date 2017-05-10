import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Http, Response, Request, XHRBackend, RequestOptions, RequestOptionsArgs, Headers } from "@angular/http";
import { AuthResponseModel, LoginModel, RegisterModel } from "./auth.models";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

let cfg = require('../config').appConfig;

@Injectable()
export class AuthService {
    private baseUrl: string;

    constructor(private http: Http) {
        this.baseUrl = cfg.apiRoute;
    }

    authenticate(loginModel: LoginModel): Observable<AuthResponseModel> {
        return this.http.post(this.baseUrl + "/account/login", loginModel)
            .map((res: Response) => res.json());
    }

    register(registerModel: RegisterModel): Observable<AuthResponseModel> {
        return this.http.post(this.baseUrl + "/account/register", registerModel)
            .map((res: Response) => res.json());
    }
}

@Injectable()
export class AuthHttpService extends Http {

    constructor(backend: XHRBackend, defaultOptions: RequestOptions) {
        super(backend, defaultOptions);
    }

    request(url: string | Request, options?: RequestOptionsArgs): Observable<Response> {
        return super.request(url, options).catch((error: Response) => {
            if (error.status === 401 || error.status === 403) {
                console.log('The authentication session expires or the user is not authorised. Force refresh of the current page.');
                window.location.href = '/login';
            }
            return Observable.throw(error);
        });
    }
}