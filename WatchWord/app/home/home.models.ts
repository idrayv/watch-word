import { Material, VocabWord } from '../material/material.models';
import { BaseResponseModel } from '../global/models';

export class AbstractWordsResponseModel extends BaseResponseModel {
    public material: Material;
    public vocabWords: VocabWord[];
}