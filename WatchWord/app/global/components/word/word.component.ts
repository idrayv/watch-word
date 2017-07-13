import { Component, OnChanges, SimpleChanges, Input } from '@angular/core';
import { WordComposition, VocabWord, VocabType } from '../../../material/material.models';
import { TranslationModalService } from '../translation-modal/translation-modal.service';

@Component({
    selector: 'word',
    templateUrl: 'app/global/components/word/word.template.html'
})

export class WordComponent implements OnChanges {

    @Input()
    public model: WordComposition;
    public classes: object = { 'word': true, 'learn-word': false, 'known-word': false };

    constructor(private translationModalService: TranslationModalService) { }

    public getTransletion(): void {
        if (!this.model.vocabWord || !this.model.vocabWord.word) {
            this.model.vocabWord = new VocabWord();
            this.model.vocabWord.word = this.model.materialWord.theWord;
        }

        this.translationModalService.pushToModal({ ...this.model.vocabWord });
    }

    ngOnChanges(changes: SimpleChanges) {
        this.classes = { 'word': true };
        if (this.model.vocabWord) {
            this.classes['learn-word'] = this.model.vocabWord.type;
            this.classes['known-word'] = !this.model.vocabWord.type;
        }
    }
}