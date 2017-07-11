import { BaseResponseModel } from '../../../global/models';
import { VocabWord } from "../../../material/material.models";

export class TranslationModalModel {
    public vocabWord: VocabWord = new VocabWord();
    public translations: Array<string> = new Array<string>();
}

export class TranslatePostResponseModel extends BaseResponseModel {
    public translations: string[];
}

export class VocabularyPostResponseModel extends BaseResponseModel { }