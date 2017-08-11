import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { VocabWordsModel, VocabWord } from '../material/material.models';
import { VocabularyPostResponseModel } from './dictionaries.models';
let cfg = require('../config').appConfig;

@Injectable()
export class DictionariesService {
    private baseUrl: string = cfg.apiRoute;

    constructor(private http: Http) { }

    public getDictionaries(): Promise<VocabWordsModel> {
        return this.http.get(this.baseUrl + '/Vocabulary').toPromise()
            .then(response => response.json())
            .catch(() => {
                return {
                    errors: ['Server error'],
                    success: false
                };
            });
    }

    public saveToVocabulary(vocabWord: VocabWord): Promise<VocabularyPostResponseModel> {
        return this.http.post(this.baseUrl + '/Vocabulary/', vocabWord).toPromise()
            .then((res: Response) => res.json())
            .catch(() => {
                return {
                    errors: ['Connection error'],
                    success: false
                };
            });
    }
}