import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs';
import { TranslationModalModel, TranslationResponseModel, VocabularyResponseModel } from './translation-modal.models';
import { VocabWord, VocabType } from '../../material.models';
import { TranslationService } from './translation.service';
import { Word } from '../../material.models';
let cfg = require('../../../config').appConfig;

@Injectable()
export class TranslationModalService {
    private baseUrl: string = cfg.apiRoute;
    private translationModalModel: Subject<TranslationModalModel> = new Subject<TranslationModalModel>();

    public constructor(private http: Http, private translationService: TranslationService) { }

    public get translationModalObservable(): Observable<TranslationModalModel> {
        return this.translationModalModel.asObservable();
    }

    public pushToModal(word: Word): void {
        this.translationService.getTransletion(word.theWord).then(response => {
            this.translationModalModel.next(this.getTransletionModel(word, response));
        });
    }

    private getTransletionModel(word: Word, serverResponse: TranslationResponseModel): TranslationModalModel {
        let modalModel: TranslationModalModel = new TranslationModalModel();
        modalModel.word = word;
        if (serverResponse.success) {
            modalModel.translations = serverResponse.translations;
        } else {
            // TODO: handle errors here
        }
        return modalModel;
    }

    public saveToVocabulary(transletionModel: TranslationModalModel): void {
        let request: VocabWord = {
            id: 0,
            type: transletionModel.isKnown ? VocabType.KnownWord : VocabType.LearnWord,
            translation: transletionModel.translation,
            word: transletionModel.word.theWord
        };

        this.translationService.saveToVocabulary(request).then(response => {
            //handle response here
        });
    }
}