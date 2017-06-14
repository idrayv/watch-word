import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Http, Response } from "@angular/http";
import { CountResponseModel } from "./materials.models";
import "rxjs/add/operator/map";
let cfg = require('../config').appConfig;

@Injectable()
export class MaterialsService {
    private baseUrl: string;

    constructor(private http: Http) {
        this.baseUrl = cfg.apiRoute;
    }

    getCount(): Observable<CountResponseModel> {
        return this.http.get(this.baseUrl + "/materials/getCount").map((res: Response) => res.json());;
    }
}