import { Injectable } from '@angular/core';
import { HttpParams } from "@angular/common/http";
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { PaginationResponseModel, CountResponseModel } from './pagination.models';
import { BaseService } from "../../base-service";

@Injectable()
export abstract class PaginationService<TEntity> extends BaseService {
    private url: string;

    constructor(entityPath: string) {
        super();
        this.url = `${this.baseUrl}/${entityPath}`;
    }

    public getCount(): Promise<CountResponseModel> {
        return this.http.get<CountResponseModel>(this.url + '/GetCount').toPromise()
            .catch(() => { return this.getConnectionError<CountResponseModel>() });
    }

    public getEntities(page: number, count: number): Promise<PaginationResponseModel<TEntity>> {
        return this.http.get<PaginationResponseModel<TEntity>>(this.url, {
            params: new HttpParams().set('page', page.toString()).set('count', count.toString())
        }).toPromise().catch(() => { return this.getConnectionError<PaginationResponseModel<TEntity>>() });
    }
}