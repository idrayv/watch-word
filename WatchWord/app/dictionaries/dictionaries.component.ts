import { Component, OnInit, OnDestroy } from '@angular/core';
import { DictionariesModel, DictionariesResponseModel } from './dictionaris.models';
import { DictionariesService } from './dictionaries.service';
import { VocabWord, VocabType } from '../material/material.models';
import { TranslationModalService } from '../global/components/translation-modal/translation-modal.service';
import { ISubscription } from 'rxjs/Subscription';

@Component({
    templateUrl: 'app/dictionaries/dictionaries.template.html',
})

export class DictionariesComponent implements OnInit, OnDestroy {
    private model: DictionariesModel = new DictionariesModel();
    private serverErrors: string[] = [];
    private modalResponse: ISubscription;

    constructor(private dictionariesService: DictionariesService, private transletionModalService: TranslationModalService) { }

    ngOnInit(): void {
        this.dictionariesService.getDictionaries().then(response => this.fillModelFromResponse(response));
        this.modalResponse = this.transletionModalService.transletionModalResponseObserverable.subscribe(response => {
            if (response.success) {
                let index = this.model.vocabWords.findIndex(n => n.word === response.vocabWord.word);
                this.model.vocabWords[index] = response.vocabWord;
            } else {
                this.serverErrors = response.errors;
            }
        });
    }

    private fillModelFromResponse(response: DictionariesResponseModel): void {
        if (response.success) {
            this.model.vocabWords = response.vocabWords;
        } else {
            this.serverErrors = response.errors;
        }
    }

    public learnWords(): VocabWord[] {
        return this.model.vocabWords.filter(word => word.type === VocabType.LearnWord);
    }

    public knownWords(): VocabWord[] {
        return this.model.vocabWords.filter(word => word.type === VocabType.KnownWord);
    }

    public getTranslation(word: VocabWord): void {
        let wordCopy: VocabWord = JSON.parse(JSON.stringify(word));
        this.transletionModalService.pushToModal(wordCopy);
    }

    ngOnDestroy(): void {
        this.modalResponse.unsubscribe();
    }
}