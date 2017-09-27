import { Injectable } from '@angular/core';
import { BaseService } from '../../../global/base-service';
import { MaterialStatisticsResponseModel } from './material-statistics.models';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class MaterialStatisticsService extends BaseService {
    constructor() {
        super();
    }

    public getRandomMaterials(url: string): Promise<MaterialStatisticsResponseModel> {
        return this.http.get<MaterialStatisticsResponseModel>(`${this.baseUrl}/${url}`).toPromise()
            .catch(() => this.getConnectionError<MaterialStatisticsResponseModel>());
    }
}
