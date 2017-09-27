import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { DictionariesService } from './dictionaries.service';
import { VocabType, VocabWord } from '../material/material.models';
import { VocabWordsResponseModel as VocabWordsModel } from '../material/material.models';
import { TranslationModalService } from '../global/components/translation-modal/translation-modal.service';
import { SpinnerService } from '../global/spinner/spinner.service';
import { BaseComponent } from '../global/base-component';

@Component({
    templateUrl: 'dictionaries.template.html'
})

export class DictionariesComponent extends BaseComponent implements OnInit, OnDestroy {
    private vocabWords: VocabWord[] = [];
    private modalResponse: ISubscription;

    constructor(private dictionariesService: DictionariesService, private spinner: SpinnerService,
        private translationModalService: TranslationModalService) {
        super();
    }

    ngOnInit(): void {
        this.spinner.displaySpinner(true);
        this.dictionariesService.getDictionaries().then((response) => {

            if (response.success) {
                this.vocabWords = response.vocabWords;
            } else {
                response.errors.forEach(err => this.displayError(err, 'Response error'));
            }

            this.spinner.displaySpinner(false);
        });

        this.modalResponse = this.translationModalService.translationModalResponseObserverable.subscribe(response => {
            this.translationModalService.updateVocabWordInCollection(response.vocabWord, this.vocabWords);
        });
    }

    public learnWords(): VocabWord[] {
        return this.vocabWords.filter(v => v.type === VocabType.LearnWord);
    }

    public knownWords(): VocabWord[] {
        return this.vocabWords.filter(v => v.type === VocabType.KnownWord);
    }

    ngOnDestroy(): void {
        this.modalResponse.unsubscribe();
    }
}