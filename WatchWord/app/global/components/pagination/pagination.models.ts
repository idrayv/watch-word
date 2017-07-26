import { BaseResponseModel } from '../../models';

export class PaginationServiceModel {
    public currentPage: number;
    public totalPages: number;
    public startPage: number;
    public endPage: number;
    public pages: number[];
    public route: string;
}

export class PaginationModel {
    public count: number;
    public currentPage: number;
    public route: string;
    public itemsPerPage: number;
}

export class PaginationResponseModel<TEntity> extends BaseResponseModel {
    public entities: TEntity[];
}

export class CountResponseModel extends BaseResponseModel {
    public count: number;
}