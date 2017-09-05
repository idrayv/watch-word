import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { BaseResponseModel } from '../global/models';
import { AbstractWordsResponseModel } from './home.models';
import { BaseService } from "../global/base-service";
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class StatisticsService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
    }

    public getRandomMaterialTopWords(url: string): Promise<AbstractWordsResponseModel> {
        return this.http.get<AbstractWordsResponseModel>(`${this.baseUrl}/statistic/${url}`).toPromise()
            .catch(() => { return this.getConnectionError<AbstractWordsResponseModel>() });
    }
}