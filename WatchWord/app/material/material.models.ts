import { BaseResponseModel } from '../abstract/models';

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
    public type: VocabType;
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
}

export class ParseResponseModel extends BaseResponseModel {
    public words: Array<Word>;
    public vocabWords: Array<VocabWord>;
}

export class CreateResponseModel extends BaseResponseModel {
    public id: number;
}

export class ImageResponseModel extends BaseResponseModel {
    public base64: string;
}