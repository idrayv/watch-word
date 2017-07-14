import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs';
import { TranslatePostResponseModel, VocabularyPostResponseModel, TranslationModalModel, TranslationModalResponseModel } from './translation-modal.models';
import { TranslationService } from './translation.service';
import { VocabWord, WordComposition } from '../../../material/material.models';
import { SpinnerService } from '../../spinner/spinner.service';
let cfg = require('../../../config').appConfig;

@Injectable()
export class TranslationModalService {
    private baseUrl: string = cfg.apiRoute;
    private translationModel: Subject<TranslationModalModel> = new Subject<TranslationModalModel>();
    private responseModel: Subject<TranslationModalResponseModel> = new Subject<TranslationModalResponseModel>();

    public constructor(private http: Http, private translationService: TranslationService, private spinner: SpinnerService) { }

    public get transletionModalResponseObserverable(): Observable<TranslationModalResponseModel> {
        return this.responseModel.asObservable();
    }

    public get translationModalObservable(): Observable<TranslationModalModel> {
        return this.translationModel.asObservable();
    }

    public pushToModal(wordComposition: WordComposition): void {
        this.spinner.displaySpinner(true);

        if (!wordComposition.vocabWord || !wordComposition.vocabWord.word) {
            wordComposition.vocabWord = { word: wordComposition.materialWord.theWord, id: 0, type: 0, translation: '' };
        }

        this.translationService.getTransletion(wordComposition.materialWord.theWord).then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                this.translationModel.next({ wordComposition: wordComposition, translations: response.translations });
            } else {
                this.responseModel.next(this.getResponseWithErrors(response.errors));
            }
        });
    }

    private getResponseWithErrors(errors: string[]): TranslationModalResponseModel {
        return { success: false, errors: errors, wordComposition: null };
    }

    public saveToVocabulary(wordComposition: WordComposition): void {
        this.spinner.displaySpinner(true);
        this.translationService.saveToVocabulary(wordComposition.vocabWord).then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                this.responseModel.next({ errors: [], wordComposition: wordComposition, success: true });
            } else {
                this.responseModel.next(this.getResponseWithErrors(response.errors));
            }
        });
    }
}