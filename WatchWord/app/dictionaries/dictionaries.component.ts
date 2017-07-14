import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { DictionariesService } from './dictionaries.service';
import { VocabWord, VocabType, WordComposition, VocabWordsModel, WordCompositionsModel } from '../material/material.models';
import { TranslationModalService } from '../global/components/translation-modal/translation-modal.service';

@Component({
    templateUrl: 'app/dictionaries/dictionaries.template.html',
})

export class DictionariesComponent implements OnInit, OnDestroy {
    private model: WordCompositionsModel = new WordCompositionsModel();
    private modalResponse: ISubscription;

    constructor(private dictionariesService: DictionariesService, private translationModalService: TranslationModalService) { }

    ngOnInit(): void {
        this.dictionariesService.getDictionaries().then(response => this.fillModelFromResponse(response));
        this.modalResponse = this.translationModalService.translationModalResponseObserverable.subscribe(response => {
            this.translationModalService.fillWordCompositionsModel(response, this.model);
        });
    }

    private fillModelFromResponse(response: VocabWordsModel): void {
        if (response.success) {
            this.model.wordCompositions = response.vocabWords.map(v => {
                return { vocabWord: v, materialWord: { theWord: v.word, count: 0, id: 0 } };
            });
        } else {
            this.model.serverErrors = response.errors;
        }
    }

    public learnWords(): WordComposition[] {
        return this.model.wordCompositions.filter(word => word.vocabWord.type === VocabType.LearnWord);
    }

    public knownWords(): WordComposition[] {
        return this.model.wordCompositions.filter(word => word.vocabWord.type === VocabType.KnownWord);
    }

    ngOnDestroy(): void {
        this.modalResponse.unsubscribe();
    }
}