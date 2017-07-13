import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { TranslatePostResponseModel, VocabularyPostResponseModel } from './translation-modal.models';
import { VocabWord } from '../../../material/material.models';
let cfg = require('../../../config').appConfig;

@Injectable()
export class TranslationService {
    private baseUrl: string = cfg.apiRoute;

    public constructor(private http: Http) { }

    public getTransletion(word: string): Promise<TranslatePostResponseModel> {
        return this.http.get(this.baseUrl + '/Translate/' + word).toPromise()
            .then((res: Response) => res.json())
            .catch(() => { return { errors: ['Connection error'], success: false, translations: [] } });
    }

    public saveToVocabulary(vocabWord: VocabWord): Promise<VocabularyPostResponseModel> {
        return this.http.post(this.baseUrl + '/Vocabulary/', vocabWord).toPromise()
            .then((res: Response) => res.json())
            .catch(() => { return { errors: ['Connection error'], success: false } });
    }
}