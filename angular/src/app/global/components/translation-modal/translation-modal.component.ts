import {Component, OnInit, OnDestroy, Input, ElementRef, HostListener, Injector, ViewChild} from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
import {TranslationModalService} from './translation-modal.service';
import {TranslationModalModel} from './translation-modal.models';
import {AppComponentBase} from '@shared/app-component-base';
import {ModalDirective} from 'ngx-bootstrap';

@Component({
    selector: 'ww-translation-modal',
    templateUrl: 'translation-modal.template.html'
})

export class TranslationModalComponent extends AppComponentBase implements OnInit, OnDestroy {
    active = false;
    @ViewChild('modal') modal: ModalDirective;
    @Input() modalId: string;
    private model: TranslationModalModel = new TranslationModalModel();
    private modelSubscription: Subscription;

    constructor(private translationModalService: TranslationModalService,
                injector: Injector,
                private componentElement: ElementRef) {
        super(injector);
    }

    show(): void {
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.modal.hide();
        this.active = false;
    }

    @HostListener('document:click', ['$event.target'])
    private documentClick(target: ElementRef): void {
        if (!this.componentElement.nativeElement.contains(target)) {
            this.close();
        }
    }

    public selectTranslation(translation: string): void {
        this.model.vocabWord.translation = translation;
        this.translationModalService.saveToVocabulary(this.model.vocabWord);
        this.close();
    }

    ngOnInit(): void {
        // TODO: Check if service is necessary
        this.modelSubscription = this.translationModalService.translationModalObservable.subscribe(model => {
            this.model = model;
            this.show();
        });
    }

    ngOnDestroy(): void {
        this.modelSubscription.unsubscribe();
    }
}
