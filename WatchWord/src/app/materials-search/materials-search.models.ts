import { Material as MaterialModel } from '../material/material.models';
import { BaseResponseModel } from '../global/models';

export class MaterialsSearchModel {
    public input = '';
    public entities: MaterialModel[] = [];
}

export class SearchResponseModel extends BaseResponseModel {
    entities: MaterialModel[];
}

export enum RequestStatus {
    NotStarted,
    InProgress,
    CompletedWithError
}
