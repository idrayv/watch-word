import {Component, OnInit, OnDestroy, Input, ElementRef, HostListener} from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
import {TranslationModalService} from './translation-modal.service';
import {ModalService} from '../modal/modal.service';
import {TranslationModalModel} from './translation-modal.models';

@Component({
    selector: 'ww-translation-modal',
    templateUrl: 'translation-modal.template.html'
})

export class TranslationModalComponent implements OnInit, OnDestroy {
    @Input() modalId: string;
    private model: TranslationModalModel = new TranslationModalModel();
    private modelSubscription: Subscription;

    constructor(private translationModalService: TranslationModalService, private modalService: ModalService,
                private componentElement: ElementRef) {
    }

    @HostListener('document:click', ['$event.target'])
    private documentClick(target: ElementRef): void {
        if (!this.componentElement.nativeElement.contains(target)) {
            this.modalService.hideModal(this.modalId);
        }
    }

    public selectTranslation(translation: string): void {
        this.model.vocabWord.translation = translation;
        this.translationModalService.saveToVocabulary(this.model.vocabWord);
        this.modalService.hideModal(this.modalId);
    }

    ngOnInit(): void {
        this.modelSubscription = this.translationModalService.translationModalObservable.subscribe(model => {
            this.model = model;
            this.modalService.showModal(this.modalId);
        });
    }

    ngOnDestroy(): void {
        this.modelSubscription.unsubscribe();
    }
}
