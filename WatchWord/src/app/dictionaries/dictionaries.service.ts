import { Injectable } from '@angular/core';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { VocabWordsResponseModel as VocabWordsModel, VocabWord } from '../material/material.models';
import { BaseService } from "../global/base-service";
import { VocabularyPostResponseModel } from './dictionaries.models';

@Injectable()
export class DictionariesService extends BaseService {
    constructor() {
        super();
    }

    public getDictionaries(): Promise<VocabWordsModel> {
        return this.http.get<VocabWordsModel>(this.baseUrl + '/Vocabulary').toPromise()
            .catch(() => { return this.getConnectionError<VocabWordsModel>() });
    }

    public saveToVocabulary(vocabWord: VocabWord): Promise<VocabularyPostResponseModel> {
        return this.http.post<VocabularyPostResponseModel>(this.baseUrl + '/Vocabulary/', vocabWord).toPromise()
            .catch(() => { return this.getConnectionError<VocabularyPostResponseModel>() });
    }
}