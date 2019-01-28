import { Component, Injector, OnInit } from '@angular/core';
import { VocabularyServiceProxy, VocabWord } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.less']
})
export class CardsComponent extends AppComponentBase implements OnInit {
  public learnWords: VocabWord[] = [];
  public randomWord: VocabWord;
  public initialized = false;
  public isTranslationVisible = false;

  constructor(private dictionariesService: VocabularyServiceProxy,
              injector: Injector) {
    super(injector);
  }

  ngOnInit() {
    abp.ui.setBusy();
    this.dictionariesService.getLearnWords()
      .finally(() => abp.ui.clearBusy())
      .subscribe((vocabWords) => {
        this.learnWords = vocabWords;
        this.initiateNewRandomWord();
        this.initialized = true;
      });
  }

  public nextWord(): void {
    this.isTranslationVisible = false;
    this.initiateNewRandomWord();
  }

  public showTranslation(): void {
    this.isTranslationVisible = true;
  }

  public correct(): void {
    // TODO: Save statistic
    this.nextWord();
  }

  public incorrect(): void {
    // TODO: Save statistic
    this.nextWord();
  }

  public markAsKnown(): void {
    // TODO: Mark as known
    this.nextWord();
  }

  private initiateNewRandomWord(): void {
    if (this.learnWords) {
      this.randomWord = this.learnWords[Math.floor(Math.random() * this.learnWords.length)];
    }
  }
}
