import { BaseResponseModel } from '../abstract/models';
import { MaterialModel } from '../material/material.models';
import { PaginationModel } from './pagination/pagination.models';

export class CountResponseModel extends BaseResponseModel {
    public count: number;
}

export class MaterialsResponseModel extends BaseResponseModel {
    public materials: MaterialModel[];
}

export class MaterialsModel {
    public paginationModel: PaginationModel = new PaginationModel();
    public materials: MaterialModel[] = [];
    public serverErrors: string[] = [];

    public get hasErrors(): boolean {
        return this.serverErrors && this.serverErrors.length !== 0;
    }
}