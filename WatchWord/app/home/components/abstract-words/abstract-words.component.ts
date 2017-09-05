import { Component, ElementRef, OnInit, OnDestroy } from '@angular/core';
import { BaseComponent } from '../../../global/base-component';
import { StatisticsService } from '../../statistics.service';
import { AbstractWordsResponseModel } from '../../home.models';
import { ControlValueAccessor } from '@angular/forms/src/forms';
import { Material, VocabWord } from '../../../material/material.models';
import { TranslationModalService } from '../../../global/components/translation-modal/translation-modal.service';
import { ISubscription } from 'rxjs/Subscription';

@Component({
    selector: 'abstract-words',
    templateUrl: 'app/home/components/abstract-words/abstract-words.template.html',
})
export class AbstractWordsComponent extends BaseComponent implements OnInit, OnDestroy {
    private material: Material;
    private vocabWords: VocabWord[];
    private translationModalResponseSubscription: ISubscription;

    constructor(
        private statisticsService: StatisticsService,
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
        this.statisticsService.getRandomMaterialTopWords(url).then(response => this.fillModel(response));
    };

    fillModel(response: AbstractWordsResponseModel): void {
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