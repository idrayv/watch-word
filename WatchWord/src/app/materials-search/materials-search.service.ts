import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { SearchResponseModel } from './materials-search.models';
import { BaseService } from '../global/base-service';


@Injectable()
export class MaterialsSearchService extends BaseService {
    constructor() {
        super();
    }

    public search(text: string): Observable<SearchResponseModel> {
        return this.http.get<SearchResponseModel>(`${this.baseUrl}/materials/search/${text}`);
    }
}
