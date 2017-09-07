import { Injectable } from '@angular/core';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { SettingsResponseModel, Setting } from './settings.models';
import { BaseResponseModel } from '../global/models';
import { BaseService } from "../global/base-service";

@Injectable()
export class SettingsService extends BaseService {
    constructor() {
        super();
    }

    public getUnfilledSiteSettings(): Promise<SettingsResponseModel> {
        return this.http.get<SettingsResponseModel>(this.baseUrl + '/settings/GetUnfilledSiteSettings').toPromise()
            .catch(() => { return this.getConnectionError<SettingsResponseModel>() });
    }

    insertSettins(settings: Setting[]): Promise<BaseResponseModel> {
        return this.http.post<BaseResponseModel>(this.baseUrl + '/settings/InsertSiteSettings', settings).toPromise()
            .catch(() => { return this.getConnectionError<BaseResponseModel>() });
    }
}