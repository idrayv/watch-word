﻿import { BaseResponseModel } from '../../../global/models';
import { VocabWord } from '../../../material/material.models';

export class TranslationModalModel {
    public vocabWord: VocabWord = new VocabWord();
    public translations: Array<string> = new Array<string>();
}

export class TranslatePostResponseModel extends BaseResponseModel {
    public translations: string[];
}

export class TransletionModalResponseModel extends BaseResponseModel {
    public vocabWord: VocabWord;
}

export class VocabularyPostResponseModel extends BaseResponseModel { }