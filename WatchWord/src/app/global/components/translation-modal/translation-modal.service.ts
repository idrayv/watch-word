import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { TranslatePostResponseModel, TranslationModalModel } from './translation-modal.models';
import { TranslationModalResponseModel } from './translation-modal.models';
import { VocabType, VocabWord } from '../../../material/material.models';
import { SpinnerService } from '../../spinner/spinner.service';
import { DictionariesService } from '../../../dictionaries/dictionaries.service';
import { BaseComponent } from '../../base-component';
import { BaseService } from '../../base-service';

@Injectable()
export class TranslationModalService extends BaseService {
    private translationModel: Subject<TranslationModalModel> = new Subject<TranslationModalModel>();
    // TODO: delete TranslationModalResponseModel, use link to original VocabWord instead
    private responseModel: Subject<TranslationModalResponseModel> = new Subject<TranslationModalResponseModel>();

    public constructor(private dictionariesService: DictionariesService, private spinner: SpinnerService) {
        super();
    }

    public get translationModalResponseObserverable(): Observable<TranslationModalResponseModel> {
        return this.responseModel.asObservable();
    }

    public get translationModalObservable(): Observable<TranslationModalModel> {
        return this.translationModel.asObservable();
    }

    public getTranslation(word: string): Promise<TranslatePostResponseModel> {
        return this.http.get<TranslatePostResponseModel>(this.baseUrl + '/Translate/' + word).toPromise()
            .catch(() =>  this.getConnectionError<TranslatePostResponseModel>());
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
                this.responseModel.next(this.getTranslationModalResponseModel(false, null, response.errors));
            }
        });
    }

    public saveToVocabulary(vocabWord: VocabWord): void {
        this.spinner.displaySpinner(true);
        this.dictionariesService.saveToVocabulary(vocabWord).then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                this.responseModel.next(this.getTranslationModalResponseModel(true, vocabWord, []));
            } else {
                this.responseModel.next(this.getTranslationModalResponseModel(false, null, response.errors));
            }
        });
    }

    // TODO: delete updateVocabWordInCollection, use link to original VocabWord instead
    public updateVocabWordInCollection(vocabWord: VocabWord, vocabWords: VocabWord[]) {
        const existingVocabWord = vocabWords.find(v => v.word === vocabWord.word);
        existingVocabWord.type = vocabWord.type;
        existingVocabWord.translation = vocabWord.translation;
    }

    private getTranslationModalResponseModel(success: boolean,
        vocabWord: VocabWord,
        errors: string[]): TranslationModalResponseModel {
        return {
            success: success,
            errors: errors,
            vocabWord: vocabWord
        };
    }
}
