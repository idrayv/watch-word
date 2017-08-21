import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { DictionariesService } from './dictionaries.service';
import { VocabType, WordComposition } from '../material/material.models';
import { VocabWordsModel } from '../material/material.models';
import { TranslationModalService } from '../global/components/translation-modal/translation-modal.service';
import { SpinnerService } from '../global/spinner/spinner.service';
import { BaseComponent } from '../global/base-component';

@Component({
    templateUrl: 'app/dictionaries/dictionaries.template.html'
})

export class DictionariesComponent extends BaseComponent implements OnInit, OnDestroy {
    private wordCompositions: WordComposition[] = [];
    private modalResponse: ISubscription;

    constructor(private dictionariesService: DictionariesService, private spinner: SpinnerService,
        private translationModalService: TranslationModalService) {
        super();
    }

    ngOnInit(): void {

        this.spinner.displaySpinner(true);
        this.dictionariesService.getDictionaries().then((response) => {
            this.fillModelFromResponse(response);
            this.spinner.displaySpinner(false);
        });

        this.modalResponse = this.translationModalService.translationModalResponseObserverable.subscribe(response => {
            this.translationModalService.fillWordCompositionsModel(response, this.wordCompositions);
        });
    }

    public learnWords(): WordComposition[] {
        return this.wordCompositions.filter(word => word.vocabWord.type === VocabType.LearnWord);
    }

    public knownWords(): WordComposition[] {
        return this.wordCompositions.filter(word => word.vocabWord.type === VocabType.KnownWord);
    }

    private fillModelFromResponse(response: VocabWordsModel): void {
        if (response.success) {
            this.wordCompositions = response.vocabWords.map(v => {
                return {
                    vocabWord: v,
                    materialWord: {
                        theWord: v.word,
                        count: 0,
                        id: 0
                    }
                };
            });
        } else {
            response.errors.forEach(err => this.displayError(err, 'Response error'));
        }
    }

    ngOnDestroy(): void {
        this.modalResponse.unsubscribe();
    }
}