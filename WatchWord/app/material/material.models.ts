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

export class Word {
    public theWord: string;
    public count: number;
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
}

export class CreateResponseModel extends BaseResponseModel {
    public id: number;
}

export class ImageResponseModel extends BaseResponseModel {
    public base64: string;
}