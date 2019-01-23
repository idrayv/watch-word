import {PaginationModel} from '../global/components/pagination/pagination.models';
import {MaterialDto} from '@shared/service-proxies/service-proxies';

export class MaterialsModel {
    public paginationModel: PaginationModel = new PaginationModel();
    public materials: MaterialDto[] = [];
}
