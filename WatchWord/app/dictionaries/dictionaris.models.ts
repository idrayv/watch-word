import { BaseResponseModel } from '../global/models';
import { VocabWord, WordComposition } from '../material/material.models';

export class DictionariesModel {
    public wordCompositions: WordComposition[] = [];
    public serverErrors: string[] = [];
}

export class DictionariesResponseModel extends BaseResponseModel {
    public vocabWords: VocabWord[];
}