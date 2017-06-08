import { Component } from '@angular/core';
import { MaterialType, MaterialModel, ParseResponseModel, Word } from "./create-material.models";
import { NgForm, NgModel } from "@angular/forms";
import { CreateMaterialService } from "./create-material.service";

@Component({
    templateUrl: "app/create-material/create-material.template.html"
})

export class CreateMaterialComponent {
    public material: MaterialModel = new MaterialModel();;
    public formSubmited = false;
    public imagePreview: string = "";

    constructor(private createMaterialService: CreateMaterialService) {
    }

    public createImagePreview(model: NgModel) {
        if (model.valid) {
            let reader = new FileReader();
            reader.onload = (e) => {
                this.imagePreview = (<any>e.target).result;
            }
            reader.readAsDataURL(<File>model.model);
        }
    }

    public validationErrors(state: NgModel): Array<string> {
        let errors: Array<string> = new Array<string>();
        let name = state.name;
        if (state.invalid) {
            for (var error in state.errors) {
                switch (error) {
                    case "minlength":
                        errors.push(`${name} must be at least ${state.errors[error].requiredLength} characters!`);
                        break;
                    case "required":
                        errors.push(`${name} must be filled in!`);
                        break;
                    case "fileType":
                        errors.push(`The content type of this attachment is not allowed. Supported types: ${state.errors[error].join(', ')}`);
                        break;
                    case "subtitlesInput":
                        (<Array<string>>state.errors[error]).forEach((er) => errors.push(er));
                        break;
                }
            }
        }
        return errors;
    }

    public createMaterial(form: NgForm): void {
        this.formSubmited = true;
        if (form.valid) {
            this.createMaterialService.createMaterial(this.material).subscribe(
                response => {
                    if (response.success) {
                        console.log('success');
                    } else {
                        console.log('error');
                    }
                },
                err => {
                    console.log('connection error');
                }
            );
            this.formSubmited = false;
            form.reset();
        }
    }
}