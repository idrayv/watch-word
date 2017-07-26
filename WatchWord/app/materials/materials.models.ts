import { BaseResponseModel } from '../global/models';
import { MaterialModel } from '../material/material.models';
import { PaginationModel } from '../global/components/pagination/pagination.models';

export class MaterialsModel {
    public paginationModel: PaginationModel = new PaginationModel();
    public materials: MaterialModel[] = [];
    public serverErrors: string[] = [];

    public get hasErrors(): boolean {
        return this.serverErrors && this.serverErrors.length !== 0;
    }
}