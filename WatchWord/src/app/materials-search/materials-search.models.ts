import { Material as MaterialModel } from '../material/material.models';
import { BaseResponseModel } from '../global/models';

export class MaterialsSearchModel {
    public input: string = '';
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