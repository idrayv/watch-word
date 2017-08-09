import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { SearchResponseModel } from './materials-search.models';
import 'rxjs/add/operator/map';

let cfg = require('../config').appConfig;

@Injectable()
export class MaterialsSearchService {
    private baseUrl: string = cfg.apiRoute;
    private connectionErrorModel = { sucess: false, errors: ['Connection error'] };

    constructor(private http: Http) { }

    public search(text: string): Observable<SearchResponseModel> {
        return this.http.get(`${this.baseUrl}/materials/search/${text}`).map(response => response.json());
    }
}