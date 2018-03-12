import {BaseResponseModel} from '../../../global/models';
import {VocabWord} from '@shared/service-proxies/service-proxies';

export class TranslationModalModel {
    public vocabWord: VocabWord = new VocabWord();
    public translations: string[] = [];
}

export class TranslationModalResponseModel extends BaseResponseModel {
    public vocabWord: VocabWord;
}
