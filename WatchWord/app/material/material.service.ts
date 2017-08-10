import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import { MaterialPostResponseModel, ParseResponseModel } from '../material/material.models';
import { ImageResponseModel, MaterialModel } from '../material/material.models';
import { MaterialResponseModel, WordComposition, Word, VocabWord, VocabType } from '../material/material.models';
import { BaseResponseModel } from '../global/models';
let cfg = require('../config').appConfig;

@Injectable()
export class MaterialService {
    private baseUrl: string = cfg.apiRoute;
    private connectionErrorModel = {
        sucess: false,
        errors: ['Connection error']
    };

    constructor(private http: Http) {
    }

    public parseSubtitles(subtitlesFile: any): Promise<ParseResponseModel> {
        let input = new FormData();
        input.append('file', subtitlesFile);

        return this.http.post(this.baseUrl + '/parse/file', input).toPromise()
            .then((res: Response) => res.json())
            .catch(() => {
                return this.connectionErrorModel;
            });
    }

    public parseImage(imageFile: any): Promise<ImageResponseModel> {
        let input = new FormData();
        input.append('file', imageFile);

        return this.http.post(this.baseUrl + '/image/parse', input).toPromise()
            .then((res: Response) => res.json())
            .catch(() => {
                return this.connectionErrorModel;
            });
    }

    public getMaterial(id: number): Promise<MaterialResponseModel> {
        return this.http.get(this.baseUrl + '/material/' + id).toPromise()
            .then((res: Response) => res.json())
            .catch(() => {
                return this.connectionErrorModel;
            });
    }

    public saveMaterial(material: MaterialModel,
                        wordCompositions: WordComposition[]): Promise<MaterialPostResponseModel> {
        material.words = wordCompositions.map(wordComposition => wordComposition.materialWord);
        return this.http.post(this.baseUrl + '/material/save', material).toPromise()
            .then((res: Response) => res.json())
            .catch(() => {
                return this.connectionErrorModel;
            });
    }

    public deleteMaterial(id: number): Promise<BaseResponseModel> {
        return this.http.delete(this.baseUrl + '/material/' + id).toPromise()
            .then((res: Response) => res.json())
            .catch(() => {
                return this.connectionErrorModel;
            });
    }

    public composeWordWithVocabulary(words: Word[], vocabWords: VocabWord[]): WordComposition[] {
        let vocabWordsObject = {};
        vocabWords.forEach(vocabWord => vocabWordsObject[vocabWord.word] = vocabWord);
        return words.map((word) => {
            return {
                materialWord: word,
                vocabWord: vocabWordsObject[word.theWord] ? vocabWordsObject[word.theWord] : {
                    id: 0,
                    type: VocabType.UnsignedWord,
                    word: word.theWord,
                    translation: ''
                }
            };
        });
    }
}