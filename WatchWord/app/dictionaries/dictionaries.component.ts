import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { DictionariesModel, DictionariesResponseModel } from './dictionaris.models';
import { DictionariesService } from './dictionaries.service';
import { VocabWord, VocabType, WordComposition } from '../material/material.models';
import { TranslationModalService } from '../global/components/translation-modal/translation-modal.service';

@Component({
    templateUrl: 'app/dictionaries/dictionaries.template.html',
})

export class DictionariesComponent implements OnInit, OnDestroy {
    private model: DictionariesModel = new DictionariesModel();
    private modalResponse: ISubscription;

    constructor(private dictionariesService: DictionariesService, private transletionModalService: TranslationModalService) { }

    ngOnInit(): void {
        this.dictionariesService.getDictionaries().then(response => this.fillModelFromResponse(response));
        // TODO: mix with the same method in material component
        this.modalResponse = this.transletionModalService.transletionModalResponseObserverable.subscribe(response => {
            if (response.success) {
                let index = this.model.vocabWords.findIndex(n => n.word === response.vocabWord.word);
                this.model.vocabWords[index] = response.vocabWord;
            } else {
                this.model.serverErrors = response.errors;
            }
        });
    }

    private fillModelFromResponse(response: DictionariesResponseModel): void {
        if (response.success) {
            this.model.vocabWords = response.vocabWords;
        } else {
            this.model.serverErrors = response.errors;
        }
    }

    public learnWords(): WordComposition[] {
        return this.model.vocabWords.filter(word => word.type === VocabType.LearnWord).map((vocab) => {
            return { vocabWord: vocab, materialWord: { theWord: vocab.word, count: 0, id: 0 } };
        });
    }

    public knownWords(): WordComposition[] {
        return this.model.vocabWords.filter(word => word.type === VocabType.KnownWord).map((vocab) => {
            return { vocabWord: vocab, materialWord: { theWord: vocab.word, count: 0, id: 0 } };
        });
    }

    ngOnDestroy(): void {
        this.modalResponse.unsubscribe();
    }
}