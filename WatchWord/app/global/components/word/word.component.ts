import { Component, Input } from '@angular/core';
import { WordComposition } from '../../../material/material.models';
import { TranslationModalService } from '../translation-modal/translation-modal.service';

@Component({
    selector: 'word',
    templateUrl: 'app/global/components/word/word.template.html'
})

export class WordComponent {

    @Input() public model: WordComposition;

    constructor(private translationModalService: TranslationModalService) { }

    public getTranslation(): void {
        this.translationModalService.pushToModal({
            vocabWord: { ...this.model.vocabWord },
            materialWord: { ...this.model.materialWord }
        });
    }
}