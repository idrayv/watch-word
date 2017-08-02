import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import { BaseResponseModel } from '../global/models';
import { SearchModel } from './materials-search.models';
let cfg = require('../config').appConfig;

@Injectable()
export class MaterialsSearchService {
    private baseUrl: string = cfg.apiRoute;
    private connectionErrorModel = { sucess: false, errors: ['Connection error'] };

    constructor(private http: Http) { }

    public search(text: string): Promise<SearchModel> {
        return this.http.get(`${this.baseUrl}/materials/search/${text}`).toPromise()
            .then((res: Response) => res.json())
            .catch(() => { return this.connectionErrorModel });
    }
}