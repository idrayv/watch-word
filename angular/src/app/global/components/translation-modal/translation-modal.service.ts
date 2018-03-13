import {Injectable} from '@angular/core';
import {Subject} from 'rxjs/Subject';
import {Observable} from 'rxjs/Observable';
import {TranslationModalModel} from './translation-modal.models';
import {TranslationServiceProxy, VocabularyServiceProxy} from '@shared/service-proxies/service-proxies';
import {VocabWord, VocabWordType} from '@shared/service-proxies/service-proxies';

@Injectable()
export class TranslationModalService {

    private translationModel: Subject<TranslationModalModel> = new Subject<TranslationModalModel>();

    // TODO: delete TranslationModalResponseModel, use link to original VocabWord instead
    private responseModel: Subject<VocabWord> = new Subject<VocabWord>();

    public constructor(private dictionariesService: VocabularyServiceProxy,
                       private translationService: TranslationServiceProxy) {
    }

    public get translationModalResponseObservable(): Observable<VocabWord> {
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

        // TODO: Do something with VocabWordType
        if (vocabWord.type === VocabWordType._2) {
            // TODO: check last user choice for unsigned words
            vocabWord.type = VocabWordType._0;
        }

        this.getTranslation(vocabWord.word)
            .finally(() => abp.ui.clearBusy('body'))
            .subscribe(translations => {
                this.translationModel.next({
                    vocabWord: vocabWord,
                    translations: translations
                });
            });
    }

    public saveToVocabulary(vocabWord: VocabWord): void {
        abp.ui.setBusy('body');
        this.dictionariesService.post(vocabWord)
            .finally(() => abp.ui.clearBusy('body'))
            .subscribe(() => this.responseModel.next(vocabWord));
    }

    // TODO: delete updateVocabWordInCollection, use link to original VocabWord instead
    public updateVocabWordInCollection(vocabWord: VocabWord, vocabWords: VocabWord[]) {
        const existingVocabWord = vocabWords.find(v => v.word === vocabWord.word);
        existingVocabWord.type = vocabWord.type;
        existingVocabWord.translation = vocabWord.translation;
    }
}
