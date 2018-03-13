import {Pipe} from '@angular/core';
import {PipeTransform} from '@angular/core';
import {VocabWordFiltration} from '../material.models';
import {VocabWord, VocabWordType} from '@shared/service-proxies/service-proxies';

@Pipe({
    name: 'vocabWordFilter'
})
export class VocabWordFiltrationPipe implements PipeTransform {
    public transform(vocabWords: VocabWord[], filters: VocabWordFiltration): VocabWord[] {
        return vocabWords.filter(vocabWord => {
            if (filters.showKnownWords && vocabWord.type === VocabWordType._1) {
                return true;
            }
            if (filters.showLearnWords && vocabWord.type === VocabWordType._0) {
                return true;
            }
            if (filters.showUnsignedWords && vocabWord.type === VocabWordType._2) {
                return true;
            }
            return false;
        });
    }
}
