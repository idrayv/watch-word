import { Component, ElementRef, OnInit, OnDestroy } from '@angular/core';
import { BaseComponent } from '../../../global/base-component';
import { WordStatisticsService } from './word-statistics.service';
import { WordStatisticsResponseModel } from './word-statistics.models';
import { ControlValueAccessor } from '@angular/forms/src/forms';
import { Material, VocabWord } from '../../../material/material.models';
import { TranslationModalService } from '../../../global/components/translation-modal/translation-modal.service';
import { ISubscription } from 'rxjs/Subscription';

@Component({
    selector: 'word-statistics',
    templateUrl: 'app/home/components/word-statistics/word-statistics.template.html',
})
export class WordStatisticsComponent extends BaseComponent implements OnInit, OnDestroy {
    private material: Material;
    private vocabWords: VocabWord[];
    private translationModalResponseSubscription: ISubscription;

    constructor(
        private statisticsService: WordStatisticsService,
        private transletionModal: TranslationModalService,
        private el: ElementRef) {
        super();
    }

    ngOnInit() {
        this.translationModalResponseSubscription = this.transletionModal
            .translationModalResponseObserverable
            .subscribe(response => {
                if (response.success) {
                    this.transletionModal.updateVocabWordInCollection(response.vocabWord, this.vocabWords);
                } else {
                    this.displayErrors(response.errors);
                }
            });

        let attribute = this.el.nativeElement.attributes.getNamedItem('url');
        var url = <string>attribute.value;
        this.statisticsService.getWordStatistics(url).then(response => this.fillModel(response));
    };

    fillModel(response: WordStatisticsResponseModel): void {
        if (response.success) {
            this.material = response.material;
            this.vocabWords = response.vocabWords;
        } else {
            this.displayErrors(response.errors);
        }
    }

    ngOnDestroy(): void {
        this.translationModalResponseSubscription.unsubscribe();
    }
}