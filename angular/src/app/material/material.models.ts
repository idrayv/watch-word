import { BaseResponseModel } from '../global/models';

export enum MaterialMode {
    Read, Edit, Add
}

export enum MaterialType {
    Film, Series
}

export enum VocabType {
    LearnWord, KnownWord, UnsignedWord, IgnoredWord
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

export class Material {
    public id: number;
    public materialType: MaterialType;
    public name: string;
    public description: string;
    public image: string;
    public words: Word[] = [];
    public owner: Account;
}

export class MaterialStats {
    public name: string;
    public value: string;
}

export class MaterialResponseModel extends BaseResponseModel {
    public material: Material;
    public vocabWords: VocabWord[];
}

export class ParseResponseModel extends BaseResponseModel {
    public words: Word[];
    public vocabWords: VocabWord[];
}

export class MaterialPostResponseModel extends BaseResponseModel {
    public id: number;
}

export class ImageResponseModel extends BaseResponseModel {
    public base64: string;
}

export class VocabWordsResponseModel extends BaseResponseModel {
    public vocabWords: VocabWord[];
}

export class VocabWordFiltration {
    private _showLearnWords: boolean;
    private _showKnownWords: boolean;
    private _showUnsignedWords: boolean;

    constructor(showLearnWords = true, showKnownWords = true, showUnsignedWords = true) {
        this._showLearnWords = showLearnWords;
        this._showKnownWords = showKnownWords;
        this._showUnsignedWords = showUnsignedWords;
    }

    private clone(): VocabWordFiltration {
        return new VocabWordFiltration(this._showLearnWords, this._showKnownWords, this._showUnsignedWords);
    }

    public get showLearnWords(): boolean {
        return this._showLearnWords;
    }

    public get showKnownWords(): boolean {
        return this._showKnownWords;
    }

    public get showUnsignedWords(): boolean {
        return this._showUnsignedWords;
    }

    public invertLearnWord(): VocabWordFiltration {
        const result = this.clone();
        result._showLearnWords = !this._showLearnWords;
        return result;
    }

    public invertKnownWord(): VocabWordFiltration {
        const result = this.clone();
        result._showKnownWords = !this._showKnownWords;
        return result;
    }

    public invertUnsignedWord(): VocabWordFiltration {
        const result = this.clone();
        result._showUnsignedWords = !this._showUnsignedWords;
        return result;
    }

}
