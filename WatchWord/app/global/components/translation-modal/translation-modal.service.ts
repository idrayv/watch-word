import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs';
import { TranslatePostResponseModel, TranslationModalModel } from './translation-modal.models';
import { TranslationModalResponseModel } from './translation-modal.models';
import { VocabType, WordComposition, WordCompositionsModel } from '../../../material/material.models';
import { SpinnerService } from '../../spinner/spinner.service';
import { DictionariesService } from '../../../dictionaries/dictionaries.service';
let cfg = require('../../../config').appConfig;

@Injectable()
export class TranslationModalService {
    private baseUrl: string = cfg.apiRoute;
    private translationModel: Subject<TranslationModalModel> = new Subject<TranslationModalModel>();
    private responseModel: Subject<TranslationModalResponseModel> = new Subject<TranslationModalResponseModel>();

    public constructor(private http: Http, private dictionariesService: DictionariesService,
                       private spinner: SpinnerService) { }

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

    public pushToModal(wordComposition: WordComposition): void {
        this.spinner.displaySpinner(true);

        if (!wordComposition.vocabWord || !wordComposition.vocabWord.word) {
            wordComposition.vocabWord = {
                word: wordComposition.materialWord.theWord,
                id: 0,
                type: 0,
                translation: ''
            };
        }

        if (wordComposition.vocabWord.type === VocabType.UnsignedWord) {
            // TODO: check last user choise for unsigned words
            wordComposition.vocabWord.type = VocabType.LearnWord;
        }

        this.getTranslation(wordComposition.materialWord.theWord).then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                this.translationModel.next({
                    wordComposition: wordComposition,
                    translations: response.translations
                });
            } else {
                this.responseModel.next(this.getResponseWithErrors(response.errors));
            }
        });
    }

    public saveToVocabulary(wordComposition: WordComposition): void {
        this.spinner.displaySpinner(true);
        this.dictionariesService.saveToVocabulary(wordComposition.vocabWord).then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                this.responseModel.next({
                    errors: [],
                    wordComposition: wordComposition,
                    success: true
                });
            } else {
                this.responseModel.next(this.getResponseWithErrors(response.errors));
            }
        });
    }

    public fillWordCompositionsModel(response: TranslationModalResponseModel, model: WordCompositionsModel): void {
        if (response.success) {
            let index = model.wordCompositions.findIndex(
                c => c.materialWord.theWord === response.wordComposition.materialWord.theWord);
            model.wordCompositions[index] = response.wordComposition;
        } else {
            model.serverErrors = response.errors;
        }
    }

    private getResponseWithErrors(errors: string[]): TranslationModalResponseModel {
        return {
            success: false,
            errors: errors,
            wordComposition: null
        };
    }
}