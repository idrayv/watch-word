import { BaseResponseModel } from '../global/models';

export enum MaterialMode {
    Read,
    Edit,
    Add
}

export enum MaterialType {
    Film,
    Series
}

export enum VocabType {
    LearnWord,
    KnownWord
}

export class Word {
    public id: number;
    public theWord: string;
    public count: number;
}

export class VocabWord {
    public id: number;
    public word: string;
    public translation: string;
    public type: VocabType = VocabType.LearnWord;
}

export class WordComposition {
    public materialWord: Word = new Word();
    public vocabWord: VocabWord = new VocabWord();
}

export class MaterialModel {
    public id: number;
    public materialType: MaterialType;
    public name: string;
    public description: string;
    public image: string;
    public words: Array<Word> = new Array<Word>();
}

export class MaterialResponseModel extends BaseResponseModel {
    public material: MaterialModel;
    public vocabWords: Array<VocabWord>;
}

export class ParseResponseModel extends BaseResponseModel {
    public words: Array<Word>;
}

export class MaterialPostResponseModel extends BaseResponseModel {
    public id: number;
}

export class ImageResponseModel extends BaseResponseModel {
    public base64: string;
}