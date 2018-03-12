import {PaginationModel} from '../global/components/pagination/pagination.models';
import {Material} from '@shared/service-proxies/service-proxies';

export class MaterialsModel {
    public paginationModel: PaginationModel = new PaginationModel();
    public materials: Material[] = [];
}
