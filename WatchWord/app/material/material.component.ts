import { NgForm, NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Subject } from "rxjs/Subject";
import { ISubscription } from "rxjs/Subscription";
import { MaterialService } from './material.service';
import { ComponentValidation } from '../abstract/component-validation';
import { MaterialModel, MaterialMode, Word } from './material.models';
import { SpinnerService } from '../spinner/spinner.service';
import { TranslationModalService } from './components/translation-modal/translation-modal.service';

@Component({
    templateUrl: 'app/material/material.template.html'
})

export class MaterialComponent extends ComponentValidation implements OnInit, OnDestroy {
    public mode: MaterialMode = null;
    public serverErrors: Array<string> = new Array<string>();
    public material: MaterialModel = new MaterialModel();
    public formSubmited = false;
    private routeSubscription: ISubscription;

    constructor(private materialService: MaterialService,
        private route: ActivatedRoute,
        private router: Router,
        private spinner: SpinnerService,
        private modalService: TranslationModalService
    ) {
        super();
    }

    ngOnInit() {
        this.routeSubscription = this.route.params.subscribe(params => this.onRouteChanged(params['id']));
    }

    private onRouteChanged(param: string): void {
        this.serverErrors = new Array<string>();

        if (param === 'create') {
            this.mode = MaterialMode.Add;
            this.material = new MaterialModel();
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
                    this.serverErrors = new Array<string>();
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
            this.materialService.saveMaterial(this.material).then(
                response => {
                    this.spinner.displaySpinner(false);
                    if (response.success) {
                        if (this.mode == MaterialMode.Add) {
                            this.router.navigateByUrl('material/' + response.id);
                        } else {
                            this.serverErrors = new Array<string>();
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

    public getTransletion(word: Word): void {
        this.modalService.pushToModal(word);
    }

    ngOnDestroy() {
        this.routeSubscription.unsubscribe();
    }
}