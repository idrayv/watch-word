import {VocabWord} from '@shared/service-proxies/service-proxies';

export class TranslationModalModel {
    public vocabWord: VocabWord = new VocabWord();
    public translations: string[] = [];
}
