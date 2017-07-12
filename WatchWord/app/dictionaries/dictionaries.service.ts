import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, URLSearchParams } from '@angular/http';
import { DictionariesResponseModel } from './dictionaris.models';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
let cfg = require('../config').appConfig;

@Injectable()
export class DictionariesService {
    private baseUrl: string = cfg.apiRoute;

    constructor(private http: Http) { }

    public getDictionaries(): Promise<DictionariesResponseModel> {
        return this.http.get(this.baseUrl + '/vocabulary').toPromise()
            .then(response => response.json())
            .catch(err => { return { errors: ['Server error'], success: false } });
    }
}