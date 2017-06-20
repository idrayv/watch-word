import { BaseResponseModel } from '../abstract/models';

export enum MaterialType {
    Film,
    Series
}

export class Word {
    public theWord: string;
    public count: number;
}

export class MaterialModel {
    public materialType: MaterialType;
    public name: string;
    public description: string;
    public image: string
    public words: Array<Word>;

}

export class ParseResponseModel extends BaseResponseModel {
    public words: Array<Word>;
}

export class ImageResponseModel extends BaseResponseModel {
    public base64: string;
}