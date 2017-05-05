import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Http, Response } from "@angular/http";
import { AuthResponseModel, LoginModel, RegisterModel } from "./auth.models";
import "rxjs/add/operator/map";
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