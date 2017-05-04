import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Http, Request, RequestMethod, RequestOptions, URLSearchParams } from "@angular/http";
import "rxjs/add/operator/map";

let cfg = require('../config').appConfig;

@Injectable()
export class AuthService {
    private baseUrl: string;

    constructor(private http: Http) {
        this.baseUrl = cfg.apiRoute;
    }

    authenticate(login: string, password: string): void {

        let data = new URLSearchParams();
        data.append('login', login);
        data.append('password', password);

        this.http
            .post(this.baseUrl + "/account/login", data)
            .subscribe(data => {
                console.log(data.json());
            }, error => {
                console.log(error.json());
            });
    }
}