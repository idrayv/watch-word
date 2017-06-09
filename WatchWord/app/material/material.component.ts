import { NgForm, NgModel } from "@angular/forms";
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { MaterialService } from "./material.service";
import { ComponentValidationService } from "../services/component-validation.service";
import { MaterialType, MaterialModel, ParseResponseModel, Word } from "./material.models";

@Component({
    templateUrl: "app/material/material.template.html"
})

export class MaterialComponent implements OnInit, OnDestroy {
    public id: number | string;
    private routeSubscriber: any;

    public material: MaterialModel = new MaterialModel();;
    public formSubmited = false;

    constructor(private createMaterialService: MaterialService, private route: ActivatedRoute, private validator: ComponentValidationService) { }

    ngOnInit() {
        this.routeSubscriber = this.route.params.subscribe(params => { this.id = +params['id']; });
    }

    ngOnDestroy() {
        this.routeSubscriber.unsubscribe();
    }

    public validationErrors(state: NgModel): Array<string> {
        return this.validator.validationErrors(state);
    }

    public createMaterial(form: NgForm): void {
        this.formSubmited = true;
        if (form.valid) {
            this.createMaterialService.createMaterial(this.material).subscribe(
                response => {
                    if (response.success) {
                        console.log('Success');
                    } else {
                        console.log('Error');
                    }
                },
                err => {
                    console.log('Connection error');
                }
            );
            this.formSubmited = false;
            form.reset();
        }
    }
}