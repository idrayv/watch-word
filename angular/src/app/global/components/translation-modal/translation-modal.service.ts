import { ElementRef, Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { TranslationModalModel } from './translation-modal.models';
import { TranslationServiceProxy, VocabularyServiceProxy } from '@shared/service-proxies/service-proxies';
import { VocabWord } from '@shared/service-proxies/service-proxies';
import { AppEnums } from '@shared/AppEnums';
import { AppSessionService } from '@shared/session/app-session.service';

@Injectable()
export class TranslationModalService {
  private originElementRef: any;
  private translationModel: Subject<TranslationModalModel> = new Subject<TranslationModalModel>();
  private responseModel: Subject<VocabWord> = new Subject<VocabWord>();

  public constructor(private vocabularyService: VocabularyServiceProxy,
                     private translationService: TranslationServiceProxy,
                     private readonly appSessionService: AppSessionService) {
  }

  public get translationModalResponseObservable(): Observable<VocabWord> {
    return this.responseModel.asObservable();
  }

  public get translationModalObservable(): Observable<TranslationModalModel> {
    return this.translationModel.asObservable();
  }

  public getTranslation(word: string): Observable<string[]> {
    return this.translationService.translate(word);
  }

  public pushToModal(vocabWord: VocabWord, elementRef: ElementRef): void {
    this.originElementRef = elementRef.nativeElement.children[0].children[1];
    const originElementRef = this.originElementRef;
    abp.ui.setBusy(originElementRef);

    if (vocabWord.type === AppEnums.VocabType.UnsignedWord) {
      vocabWord.type = this.appSessionService.lastPickedVocabType;
    }

    this.getTranslation(vocabWord.word)
      .finally(() => abp.ui.clearBusy(originElementRef))
      .subscribe(translations => {
        this.translationModel.next({
          vocabWord: vocabWord,
          translations: translations
        });
      });
  }

  public saveToVocabulary(vocabWord: VocabWord): void {
    const originElementRef = this.originElementRef;
    abp.ui.setBusy(originElementRef);
    this.appSessionService.lastPickedVocabType = vocabWord.type;
    this.vocabularyService.post(vocabWord)
      .finally(() => abp.ui.clearBusy(originElementRef))
      .subscribe(() => this.responseModel.next(vocabWord));
  }

  public updateVocabWordInCollection(vocabWord: VocabWord, vocabWords: VocabWord[]) {
    const existingVocabWord = vocabWords.find(v => v.word === vocabWord.word);
    existingVocabWord.type = vocabWord.type;
    existingVocabWord.translation = vocabWord.translation;
  }
}
