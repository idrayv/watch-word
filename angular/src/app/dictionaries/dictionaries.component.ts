import {Component, OnInit, OnDestroy, Injector} from '@angular/core';
import {ISubscription} from 'rxjs/Subscription';
import {TranslationModalService} from '../global/components/translation-modal/translation-modal.service';
import {AppComponentBase} from '@shared/app-component-base';
import {VocabularyServiceProxy, VocabWord, VocabWordType} from '@shared/service-proxies/service-proxies';

@Component({
    templateUrl: 'dictionaries.template.html'
})
export class DictionariesComponent extends AppComponentBase implements OnInit, OnDestroy {

    private vocabWords: VocabWord[] = [];
    private modalResponse: ISubscription;

    constructor(private dictionariesService: VocabularyServiceProxy,
                private translationModalService: TranslationModalService,
                injector: Injector) {
        super(injector);
    }

    ngOnInit(): void {
        abp.ui.setBusy('body');
        this.dictionariesService.get()
            .finally(() => abp.ui.clearBusy('body'))
            .subscribe((vocabWords) => this.vocabWords = vocabWords);

        this.modalResponse = this.translationModalService.translationModalResponseObservable.subscribe(vocabWord => {
            this.translationModalService.updateVocabWordInCollection(vocabWord, this.vocabWords);
        });
    }

    public learnWords(): VocabWord[] {
        return this.vocabWords.filter(v => v.type === VocabWordType._0);
    }

    public knownWords(): VocabWord[] {
        return this.vocabWords.filter(v => v.type === VocabWordType._1);
    }

    ngOnDestroy(): void {
        this.modalResponse.unsubscribe();
    }
}
