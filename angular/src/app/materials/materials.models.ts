import {Material as MaterialModel} from '../material/material.models';
import {PaginationModel} from '../global/components/pagination/pagination.models';

export class MaterialsModel {
    public paginationModel: PaginationModel = new PaginationModel();
    public materials: MaterialModel[] = [];
}
