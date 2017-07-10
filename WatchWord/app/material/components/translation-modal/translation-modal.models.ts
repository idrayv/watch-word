import { Word } from '../../material.models';
import { BaseResponseModel } from '../../../abstract/models';

export class TranslationModalModel {
    public word: Word;
    public translations: string[];
    public translation: string;
    public isKnown: boolean = false;
}

export class TranslationResponseModel extends BaseResponseModel {
    public translations: string[];
}

export class VocabularyRequestModel {
    public word: string;
    public translation: string;
    public isKnown: boolean;
}

export class VocabularyResponseModel extends BaseResponseModel {

}