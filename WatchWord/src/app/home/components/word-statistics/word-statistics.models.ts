import { BaseResponseModel } from '../../../global/models';
import { Material, VocabWord } from '../../../material/material.models';

export class WordStatisticsResponseModel extends BaseResponseModel {
    public material: Material;
    public vocabWords: VocabWord[];
}
