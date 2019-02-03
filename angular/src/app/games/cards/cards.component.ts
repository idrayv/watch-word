import { Component, Injector, OnInit } from '@angular/core';
import { LearnWord, VocabularyServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.less']
})
export class CardsComponent extends AppComponentBase implements OnInit {
  public learnWords: LearnWord[] = [];
  public randomWord: LearnWord;
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

  public increaseCorrectGuessesCount(): void {
    abp.ui.setBusy();
    this.randomWord.correctGuessesCount += 1;
    this.vocabularyService.increaseCorrectGuessesCount(this.randomWord.word)
      .finally(() => {
        this.nextWord();
        abp.ui.clearBusy();
      }).subscribe(() => {
    });
  }

  public increaseWrongGuessesCount(): void {
    abp.ui.setBusy();
    this.randomWord.wrongGuessesCount += 1;
    this.vocabularyService.increaseWrongGuessesCount(this.randomWord.word)
      .finally(() => {
        this.nextWord();
        abp.ui.clearBusy();
      }).subscribe(() => {
    });
  }

  public markAsKnown(): void {
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
