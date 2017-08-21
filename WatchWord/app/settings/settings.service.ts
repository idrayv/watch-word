import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { SettingsResponseModel, Setting } from './settings.models';
import { BaseResponseModel } from '../global/models';
let cfg = require('../config').appConfig;

@Injectable()
export class SettingsService {
    private baseUrl: string = cfg.apiRoute;
    // TODO: move connectionErrorModel to global
    private connectionErrorModel = {
        sucess: false,
        errors: ['Connection error']
    };

    constructor(private http: Http) { }

    public getUnfilledSiteSettings(): Promise<SettingsResponseModel> {
        return this.http.get(this.baseUrl + '/settings/GetUnfilledSiteSettings').toPromise()
            .then(response => response.json())
            .catch(() => { return this.connectionErrorModel; });
    }

    insertSettins(settings: Setting[]): Promise<BaseResponseModel> {
        return this.http.post(this.baseUrl + '/settings/InsertSiteSettings', settings).toPromise()
            .then((res: Response) => res.json())
            .catch(() => { return this.connectionErrorModel; });
    }
}