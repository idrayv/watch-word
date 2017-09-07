import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { BaseResponseModel } from '../../../global/models';
import { BaseService } from "../../../global/base-service";
import { WordStatisticsResponseModel } from './word-statistics.models';

@Injectable()
export class WordStatisticsService extends BaseService {
    constructor() {
        super();
    }

    public getWordStatistics(url: string): Promise<WordStatisticsResponseModel> {
        return this.http.get<WordStatisticsResponseModel>(`${this.baseUrl}/${url}`).toPromise()
            .catch(() => { return this.getConnectionError<WordStatisticsResponseModel>() });
    }
}