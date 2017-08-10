import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { MaterialService } from './material.service';
import { MaterialModel, MaterialMode, WordCompositionsModel } from './material.models';
import { ComponentValidation } from '../global/component-validation';
import { SpinnerService } from '../global/spinner/spinner.service';
import { TranslationModalService } from '../global/components/translation-modal/translation-modal.service';

@Component({
    templateUrl: 'app/material/material.template.html'
})

export class MaterialComponent extends ComponentValidation implements OnInit, OnDestroy {
    public mode: MaterialMode = null;
    public model: WordCompositionsModel = new WordCompositionsModel();
    public material: MaterialModel = new MaterialModel();
    public formSubmited = false;
    private routeSubscription: ISubscription;
    private modalResponse: ISubscription;

    constructor(private materialService: MaterialService, private route: ActivatedRoute, private router: Router,
                private spinner: SpinnerService, private translationModalService: TranslationModalService) {
        super();
    }

    ngOnInit() {
        this.routeSubscription = this.route.params.subscribe(params => this.onRouteChanged(params['id']));
        this.modalResponse = this.translationModalService.translationModalResponseObserverable.subscribe(response => {
            this.translationModalService.fillWordCompositionsModel(response, this.model);
        });
    }

    private onRouteChanged(param: string): void {
        this.model.serverErrors = [];

        if (param === 'create') {
            this.mode = MaterialMode.Add;
            this.material = new MaterialModel();
            this.model.wordCompositions = [];
        } else if (+param) {
            this.initializeMaterial(+param);
        } else {
            console.log('404');
            // TODO: redirect to 404
        }
    }

    private initializeMaterial(id: number): void {
        this.spinner.displaySpinner(true);
        this.materialService.getMaterial(id).then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                this.mode = MaterialMode.Read;
                this.material = response.material;
                this.model.wordCompositions = this.materialService.composeWordWithVocabulary(this.material.words,
                    response.vocabWords);
                this.model.serverErrors = [];
            } else {
                this.mode = null;
                this.model.serverErrors = response.errors;
            }
        });
    }

    public editMaterial(): void {
        this.mode = MaterialMode.Edit;
    }

    public deleteMaterial(): void {
        this.spinner.displaySpinner(true);
        this.materialService.deleteMaterial(this.material.id).then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                this.router.navigateByUrl('materials');
            } else {
                response.errors.forEach(err => console.log(err));
                this.model.serverErrors = response.errors;
            }
        });
    }

    public saveMaterial(form: NgForm): void {
        this.formSubmited = true;
        if (form.valid) {
            this.spinner.displaySpinner(true);
            this.materialService.saveMaterial(this.material, this.model.wordCompositions).then(response => {
                this.spinner.displaySpinner(false);
                if (response.success) {
                    if (this.mode == MaterialMode.Add) {
                        this.router.navigateByUrl('material/' + response.id);
                    } else {
                        this.model.serverErrors = [];
                        this.mode = MaterialMode.Read;
                    }
                } else {
                    response.errors.forEach(err => console.log(err));
                    this.model.serverErrors = response.errors;
                }
            });
            this.formSubmited = false;
        }
    }

    ngOnDestroy() {
        this.routeSubscription.unsubscribe();
        this.modalResponse.unsubscribe();
    }
}