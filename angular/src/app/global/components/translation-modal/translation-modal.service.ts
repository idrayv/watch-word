import {Injectable} from '@angular/core';
import {Subject} from 'rxjs/Subject';
import {Observable} from 'rxjs/Observable';
import {TranslationModalModel} from './translation-modal.models';
import {TranslationModalResponseModel} from './translation-modal.models';
import {VocabType, VocabWord} from '../../../material/material.models';
import {DictionariesService} from '../../../dictionaries/dictionaries.service';
import {TranslationServiceProxy} from '@shared/service-proxies/service-proxies';

@Injectable()
export class TranslationModalService {
    private translationModel: Subject<TranslationModalModel> = new Subject<TranslationModalModel>();
    // TODO: delete TranslationModalResponseModel, use link to original VocabWord instead
    private responseModel: Subject<TranslationModalResponseModel> = new Subject<TranslationModalResponseModel>();

    public constructor(private dictionariesService: DictionariesService,
                       private translationService: TranslationServiceProxy) {
    }

    public get translationModalResponseObservable(): Observable<TranslationModalResponseModel> {
        return this.responseModel.asObservable();
    }

    public get translationModalObservable(): Observable<TranslationModalModel> {
        return this.translationModel.asObservable();
    }

    public getTranslation(word: string): Observable<string[]> {
        return this.translationService.translate(word);
    }

    public pushToModal(vocabWord: VocabWord): void {
        abp.ui.setBusy('body');

        if (vocabWord.type === VocabType.UnsignedWord) {
            // TODO: check last user choice for unsigned words
            vocabWord.type = VocabType.LearnWord;
        }

        this.getTranslation(vocabWord.word).finally(() => abp.ui.clearBusy('body')).subscribe(translations => {
            this.translationModel.next({
                vocabWord: vocabWord,
                translations: translations
            });
        });
    }

    public saveToVocabulary(vocabWord: VocabWord): void {
        abp.ui.setBusy('body');
        this.dictionariesService.saveToVocabulary(vocabWord).then(response => {
            abp.ui.clearBusy('body');
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

    private getTranslationModalResponseModel(success: boolean, vocabWord: VocabWord, errors: string[]): TranslationModalResponseModel {
        return {
            success: success,
            errors: errors,
            vocabWord: vocabWord
        };
    }
}
