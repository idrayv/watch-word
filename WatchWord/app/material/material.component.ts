import { NgForm, NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { ISubscription } from 'rxjs/Subscription';
import { MaterialService } from './material.service';
import { MaterialModel, MaterialMode, Word, WordComposition, VocabWord } from './material.models';
import { ComponentValidation } from '../global/component-validation';
import { SpinnerService } from '../global/spinner/spinner.service';
import { TranslationModalService } from '../global/components/translation-modal/translation-modal.service';

@Component({
    templateUrl: 'app/material/material.template.html'
})

export class MaterialComponent extends ComponentValidation implements OnInit, OnDestroy {
    public mode: MaterialMode = null;
    public serverErrors: string[] = [];
    public material: MaterialModel = new MaterialModel();
    public wordCompositions: WordComposition[] = [];
    public formSubmited = false;
    private routeSubscription: ISubscription;
    private modalResponse: ISubscription;

    constructor(private materialService: MaterialService,
        private route: ActivatedRoute,
        private router: Router,
        private spinner: SpinnerService,
        private transletionModalService: TranslationModalService
    ) {
        super();
    }

    ngOnInit() {
        this.routeSubscription = this.route.params.subscribe(params => this.onRouteChanged(params['id']));
        // TODO: mix with the same method in dictionaries component
        this.modalResponse = this.transletionModalService.transletionModalResponseObserverable.subscribe(response => {
            if (response.success) {
                let index = this.wordCompositions.findIndex(c => c.materialWord.theWord === response.vocabWord.word);
                this.wordCompositions[index].vocabWord = response.vocabWord;
            } else {
                this.serverErrors = response.errors;
            }
        });
    }

    private onRouteChanged(param: string): void {
        this.serverErrors = [];

        if (param === 'create') {
            this.mode = MaterialMode.Add;
            this.material = new MaterialModel();
            this.wordCompositions = [];
        } else if (+param) {
            this.initializeMaterial(+param);
        } else {
            console.log('404');
            // TODO: redirect to 404
        }
    }

    private initializeMaterial(id: number): void {
        this.spinner.displaySpinner(true);
        this.materialService.getMaterial(id).then(
            response => {
                this.spinner.displaySpinner(false);
                if (response.success) {
                    this.mode = MaterialMode.Read;
                    this.material = response.material;
                    this.wordCompositions = this.materialService.composeWordWithVocabulary(this.material.words, response.vocabWords);
                    this.serverErrors = [];
                } else {
                    this.mode = null;
                    this.serverErrors = response.errors;
                }
            }
        );
    }

    public editMaterial(): void {
        this.mode = MaterialMode.Edit;
    }

    public deleteMaterial(): void {
        this.spinner.displaySpinner(true);
        this.materialService.deleteMaterial(this.material.id).then(
            response => {
                this.spinner.displaySpinner(false);
                if (response.success) {
                    this.router.navigateByUrl('materials');
                } else {
                    response.errors.forEach(err => console.log(err));
                    this.serverErrors = response.errors;
                }
            }
        );
    }

    public saveMaterial(form: NgForm): void {
        this.formSubmited = true;
        if (form.valid) {
            this.spinner.displaySpinner(true);
            this.materialService.saveMaterial(this.material, this.wordCompositions).then(
                response => {
                    this.spinner.displaySpinner(false);
                    if (response.success) {
                        if (this.mode == MaterialMode.Add) {
                            this.router.navigateByUrl('material/' + response.id);
                        } else {
                            this.serverErrors = [];
                            this.mode = MaterialMode.Read;
                        }
                    } else {
                        response.errors.forEach(err => console.log(err));
                        this.serverErrors = response.errors;
                    }
                }
            );
            this.formSubmited = false;
        }
    }

    ngOnDestroy() {
        this.routeSubscription.unsubscribe();
        this.modalResponse.unsubscribe();
    }
}