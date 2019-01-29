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

  constructor(private vocabularyService: VocabularyServiceProxy,
              injector: Injector) {
    super(injector);
  }

  ngOnInit() {
    abp.ui.setBusy();
    this.vocabularyService.getLearnWords()
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
    abp.message.confirm('Do you really want to mark \'' + this.randomWord.word
      + '\' as a known word? This action will remove this word from the flashcard game.',
      this.performMarkAsKnown.bind(this));
  }

  private performMarkAsKnown(isConfirmed) {
    if (isConfirmed) {
      abp.ui.setBusy();
      this.vocabularyService.markAsKnown([this.randomWord.word])
        .finally(() => {
          this.nextWord();
          abp.ui.clearBusy();
        }).subscribe(() => {
      });
    }
  }

  private initiateNewRandomWord(): void {
    if (this.learnWords) {
      this.randomWord = this.learnWords[Math.floor(Math.random() * this.learnWords.length)];
    }
  }
}
