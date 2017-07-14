import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { TranslationModalService } from './translation-modal.service';
import { ModalService } from '../modal/modal.service';
import { TranslationModalModel } from './translation-modal.models';

@Component({
    selector: 'translation-modal',
    templateUrl: 'app/global/components/translation-modal/translation-modal.template.html'
})

export class TranslationModalComponent implements OnInit, OnDestroy {
    @Input()
    modalId: string;

    private model: TranslationModalModel = new TranslationModalModel();
    private modelSubscription: Subscription;

    constructor(private transletionModalService: TranslationModalService, private modalService: ModalService) { }

    public selectTranslation(translation: string): void {
        this.model.wordComposition.vocabWord.translation = translation;
        this.transletionModalService.saveToVocabulary(this.model.wordComposition);
        this.modalService.hideModal(this.modalId);
    }

    ngOnInit(): void {
        this.modelSubscription = this.transletionModalService.translationModalObservable.subscribe(model => {
            this.model = model;
            this.modalService.showModal(this.modalId);
        });
    }

    ngOnDestroy(): void {
        this.modelSubscription.unsubscribe();
    }
}