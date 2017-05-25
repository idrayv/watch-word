export class MaterialModel {
    public materialType: MaterialType;
    public name: string;
    public description: string;
    public image: File
    public words: Array<Word>;
}

export class Word {
    public theWord: string;
    public count: number;
}

export enum MaterialType {
    Film,
    Series
}

export class ParseResponseModel {
    public succeeded: boolean;
    public errors: string[];
    public words: Array<Word>;
}