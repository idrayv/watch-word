import { Pipe } from '@angular/core';
import { PipeTransform } from '@angular/core';
import { VocabWordFiltration } from '../material.models';
import { VocabWord } from '@shared/service-proxies/service-proxies';
import { AppEnums } from '@shared/AppEnums';

@Pipe({
  name: 'vocabWordFilter'
})
export class VocabWordFiltrationPipe implements PipeTransform {
  public transform(vocabWords: VocabWord[], filters: VocabWordFiltration): VocabWord[] {
    return vocabWords.filter(vocabWord => {
      if (filters.showKnownWords && vocabWord.type === AppEnums.VocabType.KnownWord) {
        return true;
      }

      if (filters.showLearnWords && vocabWord.type === AppEnums.VocabType.LearnWord) {
        return true;
      }

      if (filters.showUnsignedWords && vocabWord.type === AppEnums.VocabType.UnsignedWord) {
        return true;
      }

      return false;
    });
  }
}
