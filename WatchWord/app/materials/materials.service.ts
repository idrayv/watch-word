import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Http, Response, RequestOptions, URLSearchParams } from "@angular/http";
import { CountResponseModel, MaterialsResponseModel } from "./materials.models";
import "rxjs/add/operator/catch";
import 'rxjs/add/operator/toPromise';

let cfg = require('../config').appConfig;

@Injectable()
export class MaterialsService {
    private baseUrl: string;

    constructor(private http: Http) {
        this.baseUrl = cfg.apiRoute;
    }

    public getCount(): Promise<CountResponseModel> {
        return this.http.get(this.baseUrl + "/materials/getCount")
            .toPromise()
            .then(response => response.json())
            .catch(err => { return { errors: ["Server error"], success: false, count: 0 } });
    }

    public getMaterials(page: number, count: number): Promise<MaterialsResponseModel> {
        let requestOptions: RequestOptions = new RequestOptions();
        let params: URLSearchParams = new URLSearchParams();

        params.set("page", page.toString());
        params.set("count", count.toString());
        requestOptions.search = params;

        return this.http.get(this.baseUrl + "/materials/GetMaterials", requestOptions)
            .toPromise()
            .then(response => response.json())
            .catch(() => { return { errors: ["Server error"], success: false, materials: [] } });
    }
}