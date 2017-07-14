import { BaseResponseModel } from '../../../global/models';
import { WordComposition } from '../../../material/material.models';

export class TranslationModalModel {
    public wordComposition: WordComposition = new WordComposition();
    public translations: string[] = [];
}

export class TranslatePostResponseModel extends BaseResponseModel {
    public translations: string[];
}

export class TranslationModalResponseModel extends BaseResponseModel {
    public wordComposition: WordComposition;
}