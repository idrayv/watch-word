import {Component, ElementRef, Input} from '@angular/core';
import {TranslationModalService} from '../translation-modal/translation-modal.service';
import {VocabWord, Word} from '@shared/service-proxies/service-proxies';
import {AppEnums} from '@shared/AppEnums';

@Component({
    selector: 'ww-word',
    templateUrl: 'word.template.html'
})
export class WordComponent {
    @Input() public vocabWord: VocabWord;
    @Input() public word: Word;
    @Input() public batchSelect: boolean;
    @Input() public markedAsKnownBatch: string[];

    constructor(
        private translationModalService: TranslationModalService,
        private elementRef: ElementRef) {
    }

    wordClick(): void {
        if (this.batchSelect) {
            if (this.vocabWord.type === AppEnums.VocabType.UnsignedWord) {
                const position = this.markedAsKnownBatch.indexOf(this.vocabWord.word);
                if (position === -1) {
                    this.markedAsKnownBatch.push(this.vocabWord.word);
                } else {
                    this.markedAsKnownBatch.splice(position, 1);
                }
            }
        } else {
            this.getTranslation();
        }
    }

    public getTranslation(): void {
        this.translationModalService.pushToModal(this.vocabWord.clone(), this.elementRef);
    }
}
