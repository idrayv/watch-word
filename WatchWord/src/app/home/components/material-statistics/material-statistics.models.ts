import { BaseResponseModel } from '../../../global/models';
import { Material } from '../../../material/material.models';

export class MaterialStatisticsResponseModel extends BaseResponseModel {
    public materials: Material[];
}
