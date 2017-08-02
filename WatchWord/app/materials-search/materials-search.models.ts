import { MaterialModel } from '../material/material.models';
import { BaseResponseModel } from '../global/models';

export class MaterialsSearchModel {
    public input: string = "";
    public entities: MaterialModel[] = [];
}

export class SearchModel extends BaseResponseModel {
    entities: MaterialModel[];
}
