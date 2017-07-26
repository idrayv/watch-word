import { Http, RequestOptions, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { PaginationResponseModel, CountResponseModel } from './pagination.models';
let cfg = require('../../../config.js').appConfig;

export abstract class PaginationService<TEntity> {
    private url: string;

    constructor(private http: Http, entityPath: string) {
        this.url = `${cfg.apiRoute}/${entityPath}`;
    }

    public getCount(): Promise<CountResponseModel> {
        return this.http.get(this.url + '/GetCount').toPromise()
            .then(response => response.json())
            .catch(err => { return { errors: ['Server error'], success: false, count: 0 } });
    }

    public getEntities(page: number, count: number): Promise<PaginationResponseModel<TEntity>> {
        let requestOptions: RequestOptions = new RequestOptions();
        let params: URLSearchParams = new URLSearchParams();

        params.set('page', page.toString());
        params.set('count', count.toString());
        requestOptions.search = params;

        return this.http.get(this.url, requestOptions).toPromise()
            .then(response => response.json())
            .catch(() => { return { errors: ['Server error'], success: false, materials: [] } });
    }
}