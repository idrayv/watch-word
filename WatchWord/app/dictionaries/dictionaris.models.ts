import { BaseResponseModel } from '../global/models';
import { VocabWord } from '../material/material.models';

export class DictionariesModel {
    public vocabWords: VocabWord[] = [];
    public serverErrors: string[] = [];
}

export class DictionariesResponseModel extends BaseResponseModel {
    public vocabWords: VocabWord[];
}