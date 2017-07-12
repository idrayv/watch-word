import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs';
import { TranslatePostResponseModel, VocabularyPostResponseModel, TranslationModalModel, TranslationModalResponseModel } from './translation-modal.models';
import { TranslationService } from './translation.service';
import { VocabWord } from '../../../material/material.models';
let cfg = require('../../../config').appConfig;

@Injectable()
export class TranslationModalService {
    private baseUrl: string = cfg.apiRoute;
    private translationModel: Subject<TranslationModalModel> = new Subject<TranslationModalModel>();
    private responseModel: Subject<TranslationModalResponseModel> = new Subject<TranslationModalResponseModel>();

    public constructor(private http: Http, private translationService: TranslationService) { }

    public get transletionModalResponseObserverable(): Observable<TranslationModalResponseModel> {
        return this.responseModel.asObservable();
    }

    public get translationModalObservable(): Observable<TranslationModalModel> {
        return this.translationModel.asObservable();
    }

    public pushToModal(vocabWord: VocabWord): void {
        this.translationService.getTransletion(vocabWord.word).then(response => {
            if (response.success) {
                this.translationModel.next({ vocabWord: vocabWord, translations: response.translations });
            } else {
                this.responseModel.next(this.getResponseWithErrors(response.errors));
            }
        });
    }

    private getResponseWithErrors(errors: string[]): TranslationModalResponseModel {
        return {
            success: false,
            errors: errors,
            vocabWord: null
        };
    }

    public saveToVocabulary(vocabWord: VocabWord): void {
        this.translationService.saveToVocabulary(vocabWord).then(response => {
            if (response.success) {
                this.responseModel.next({ errors: [], vocabWord: vocabWord, success: true });
            } else {
                this.responseModel.next(this.getResponseWithErrors(response.errors));
            }
        });
    }
}