import { Injectable } from '@angular/core';
import { BaseResponseModel } from '../../../global/models';
import { BaseService } from '../../../global/base-service';
import { WordStatisticsResponseModel } from './word-statistics.models';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';

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