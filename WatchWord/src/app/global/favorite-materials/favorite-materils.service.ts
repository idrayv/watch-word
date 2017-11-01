import { Injectable } from '@angular/core';
import { BaseService } from '../base-service';
import { BaseResponseModel } from '../models';
import { GetFavoriteMaterialResponseModel } from './FavoriteMaterialsModels';
import { HttpParams } from '@angular/common/http';


@Injectable()
export class FavoriteMaterialsService extends BaseService {
    constructor() {
        super();
    }

    public add(id: number): Promise<BaseResponseModel> {
        const input = new FormData();
        input.append('materialId', id.toString());

        return this.http.post<BaseResponseModel>(this.baseUrl + '/favoriteMaterial/', input).toPromise()
            .catch(() => this.getConnectionError<BaseResponseModel>());
    }

    public delete(id: number): Promise<BaseResponseModel> {

        return this.http.delete<BaseResponseModel>(this.baseUrl + '/favoriteMaterial/' + id.toString()).toPromise()
            .catch(() => this.getConnectionError<BaseResponseModel>());
    }

    public get(id: number): Promise<GetFavoriteMaterialResponseModel> {
        return this.http.get<GetFavoriteMaterialResponseModel>(this.baseUrl + '/favoriteMaterial/' + id).toPromise()
            .catch(() => this.getConnectionError<GetFavoriteMaterialResponseModel>());
    }
}
