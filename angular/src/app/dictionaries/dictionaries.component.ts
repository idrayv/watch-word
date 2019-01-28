import { Component, OnInit, OnDestroy, Injector } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { TranslationModalService } from '../global/components/translation-modal/translation-modal.service';
import { AppComponentBase } from '@shared/app-component-base';
import { VocabularyServiceProxy, VocabWord } from '@shared/service-proxies/service-proxies';
import { AppEnums } from '@shared/AppEnums';

@Component({
  templateUrl: 'dictionaries.template.html'
})
export class DictionariesComponent extends AppComponentBase implements OnInit, OnDestroy {

  public learnWordsCount = 0;
  public knownWordsCount = 0;

  private vocabWords: VocabWord[] = [];
  private modalResponse: ISubscription;

  constructor(private dictionariesService: VocabularyServiceProxy,
              private translationModalService: TranslationModalService,
              injector: Injector) {
    super(injector);
  }

  ngOnInit(): void {
    abp.ui.setBusy();
    this.dictionariesService.get()
      .finally(() => abp.ui.clearBusy())
      .subscribe((vocabWords) => {
        this.vocabWords = vocabWords;
        this.learnWordsCount = this.vocabWords.filter(v => v.type === AppEnums.VocabType.LearnWord).length;
        this.knownWordsCount = this.vocabWords.filter(v => v.type === AppEnums.VocabType.KnownWord).length;
      });

    this.modalResponse = this.translationModalService.translationModalResponseObservable.subscribe(vocabWord => {
      this.translationModalService.updateVocabWordInCollection(vocabWord, this.vocabWords);
    });
  }

  public learnWords(): VocabWord[] {
    return this.vocabWords.filter(v => v.type === AppEnums.VocabType.LearnWord);
  }

  public knownWords(): VocabWord[] {
    return this.vocabWords.filter(v => v.type === AppEnums.VocabType.KnownWord);
  }

  ngOnDestroy(): void {
    this.modalResponse.unsubscribe();
  }
}
