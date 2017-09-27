import { Pipe } from "@angular/core";
import { VocabWord, VocabWordFiltration, VocabType } from '../material.models';

@Pipe({
    name: "vocabWordFilter"
})
export class VocabWordFiltrarionPipe {
    public transform(vocabWords: VocabWord[], filters: VocabWordFiltration): VocabWord[] {
        return vocabWords.filter(vocabWord => {
            if (filters.showKnownWords && vocabWord.type === VocabType.KnownWord)
                return true;
            if (filters.showLearnWords && vocabWord.type === VocabType.LearnWord)
                return true;
            if (filters.showUnsignedWords && vocabWord.type === VocabType.UnsignedWord)
                return true;
            return false;
        });
    }
}