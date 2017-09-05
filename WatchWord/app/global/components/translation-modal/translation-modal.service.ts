import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs';
import { TranslatePostResponseModel, TranslationModalModel } from './translation-modal.models';
import { TranslationModalResponseModel } from './translation-modal.models';
import { VocabType, VocabWord } from '../../../material/material.models';
import { SpinnerService } from '../../spinner/spinner.service';
import { DictionariesService } from '../../../dictionaries/dictionaries.service';
import { BaseComponent } from '../../base-component';
let cfg = require('../../../config').appConfig;

@Injectable()
export class TranslationModalService {
    private baseUrl: string = cfg.apiRoute;
    private translationModel: Subject<TranslationModalModel> = new Subject<TranslationModalModel>();
    private responseModel: Subject<TranslationModalResponseModel> = new Subject<TranslationModalResponseModel>();

    public constructor(private http: Http, private dictionariesService: DictionariesService,
        private spinner: SpinnerService) {
    }

    public get translationModalResponseObserverable(): Observable<TranslationModalResponseModel> {
        return this.responseModel.asObservable();
    }

    public get translationModalObservable(): Observable<TranslationModalModel> {
        return this.translationModel.asObservable();
    }

    public getTranslation(word: string): Promise<TranslatePostResponseModel> {
        return this.http.get(this.baseUrl + '/Translate/' + word).toPromise()
            .then((res: Response) => res.json())
            .catch(() => {
                return {
                    errors: ['Connection error'],
                    success: false,
                    translations: []
                };
            });
    }

    public pushToModal(vocabWord: VocabWord): void {
        this.spinner.displaySpinner(true);

        if (vocabWord.type === VocabType.UnsignedWord) {
            // TODO: check last user choise for unsigned words
            vocabWord.type = VocabType.LearnWord;
        }

        this.getTranslation(vocabWord.word).then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                this.translationModel.next({
                    vocabWord: vocabWord,
                    translations: response.translations
                });
            } else {
                this.responseModel.next(this.getResponseWithErrors(response.errors));
            }
        });
    }

    public saveToVocabulary(vocabWord: VocabWord): void {
        this.spinner.displaySpinner(true);
        this.dictionariesService.saveToVocabulary(vocabWord).then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                this.responseModel.next({
                    errors: [],
                    vocabWord: vocabWord,
                    success: true
                });
            } else {
                this.responseModel.next(this.getResponseWithErrors(response.errors));
            }
        });
    }

    public updateVocabWordInCollection(vocabWord: VocabWord, vocabWords: VocabWord[]) {
        let existingVocabWord = vocabWords.find(v => v.word === vocabWord.word);
        existingVocabWord.type = vocabWord.type;
        existingVocabWord.translation = vocabWord.translation;
    }

    private getResponseWithErrors(errors: string[]): TranslationModalResponseModel {
        return {
            success: false,
            errors: errors,
            vocabWord: null
        };
    }
}