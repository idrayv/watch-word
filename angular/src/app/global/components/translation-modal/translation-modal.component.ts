import {Component, OnInit, OnDestroy, Injector, ViewChild} from '@angular/core';
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

    @ViewChild('modal') modal: ModalDirective;
    active = false;

    private model: TranslationModalModel = new TranslationModalModel();
    private modelSubscription: Subscription;

    constructor(injector: Injector,
                private translationModalService: TranslationModalService) {
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

    public selectTranslation(translation: string): void {
        if (!this.appSession.userId) {
          abp.message.info('Please authorize in order to populate your vocabulary');
        } else {
          this.model.vocabWord.translation = translation;
          this.translationModalService.saveToVocabulary(this.model.vocabWord);
        }

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
