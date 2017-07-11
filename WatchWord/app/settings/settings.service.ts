import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { SettingsResponseModel, Setting } from './settings.models';
import { BaseResponseModel } from '../global/models';
let cfg = require('../config').appConfig;

@Injectable()
export class SettingsService {
    private baseUrl: string = cfg.apiRoute;
    private connectionErrorModel = { sucess: false, errors: ['Connection error'] };

    constructor(private http: Http) { }

    public getUnfilledSiteSettings(): Promise<SettingsResponseModel> {
        return this.http.get(this.baseUrl + '/settings/GetUnfilledSiteSettings').toPromise()
            .then(response => response.json())
            .catch(err => { return { errors: ['Server error'], success: false, settings: [] } });
    }

    insertSettins(settings: Setting[]): Promise<BaseResponseModel> {
        return this.http.post(this.baseUrl + '/settings/InsertSiteSettings', settings).toPromise()
            .then((res: Response) => res.json())
            .catch(() => { return this.connectionErrorModel });
    }
}