import {Component, OnInit, OnDestroy, Injector} from '@angular/core';
import {ISubscription} from 'rxjs/Subscription';
import {DictionariesService} from './dictionaries.service';
import {VocabType, VocabWord} from '../material/material.models';
import {TranslationModalService} from '../global/components/translation-modal/translation-modal.service';
import {AppComponentBase} from '@shared/app-component-base';

@Component({
    templateUrl: 'dictionaries.template.html'
})

export class DictionariesComponent extends AppComponentBase implements OnInit, OnDestroy {
    private vocabWords: VocabWord[] = [];
    private modalResponse: ISubscription;

    constructor(private dictionariesService: DictionariesService,
                private translationModalService: TranslationModalService,
                injector: Injector) {
        super(injector);
    }

    ngOnInit(): void {
        abp.ui.setBusy('body');
        this.dictionariesService.getDictionaries().then((response) => {

            if (response.success) {
                this.vocabWords = response.vocabWords;
            } else {
                response.errors.forEach(err => this.displayError(err));
            }

            abp.ui.clearBusy('body');
        });

        this.modalResponse = this.translationModalService.translationModalResponseObservable.subscribe(response => {
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
