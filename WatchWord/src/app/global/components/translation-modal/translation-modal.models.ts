import { BaseResponseModel } from '../../../global/models';
import { VocabWord } from '../../../material/material.models';

export class TranslationModalModel {
    public vocabWord: VocabWord = new VocabWord();
    public translations: string[] = [];
}

export class TranslatePostResponseModel extends BaseResponseModel {
    public translations: string[];
}

export class TranslationModalResponseModel extends BaseResponseModel {
    public vocabWord: VocabWord;
}
