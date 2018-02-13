import {Injectable} from '@angular/core';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import {PaginationResponseModel, CountResponseModel} from './pagination.models';

@Injectable()
export abstract class PaginationService<TEntity> {
    private url: string;

    constructor(entityPath: string) {
    }

    public getCount(): Promise<CountResponseModel> {
        /*return this.http.get<CountResponseModel>(this.url + '/GetCount').toPromise()
            .catch(() => this.getConnectionError<CountResponseModel>());*/
        return new Promise(function(resolve) {
            resolve(new CountResponseModel);
        })
    }

    public getEntities(page: number, count: number): Promise<PaginationResponseModel<TEntity>> {
        /*return this.http.get<PaginationResponseModel<TEntity>>(this.url, {
            params: new HttpParams().set('page', page.toString()).set('count', count.toString())
        }).toPromise().catch(() => this.getConnectionError<PaginationResponseModel<TEntity>>());*/
        return new Promise(function(resolve) {
            resolve(new PaginationResponseModel<TEntity>());
        })
    }
}
