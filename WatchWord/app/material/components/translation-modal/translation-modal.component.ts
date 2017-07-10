import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { TranslationModalService } from './translation-modal.service';
import { TranslationModalModel } from './translation-modal.models';
import { ModalService } from '../modal/modal.service';

@Component({
    selector: 'translation-modal',
    templateUrl: 'app/material/components/translation-modal/translation-modal.template.html'
})

export class TranslationModalComponent implements OnInit, OnDestroy {
    @Input()
    modalId: string;

    private model: TranslationModalModel = new TranslationModalModel();
    private modelSubscription: Subscription;

    constructor(private transletionModalService: TranslationModalService, private modalService: ModalService) { }

    public selectTranslation(translation: string): void {
        this.model.translation =  translation;
        this.transletionModalService.saveToVocabulary(this.model);
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