import { Component } from '@angular/core';
import { MaterialType, MaterialModel, ParseResponseModel, Word } from "./create-material.models";
import { NgForm, NgModel } from "@angular/forms";
import { CreateMaterialService } from "./create-material.service";

@Component({
    templateUrl: "app/create-material/create-material.template.html"
})

export class CreateMaterialComponent {
    public material: MaterialModel;
    public imagePreview: string;
    public imageError: string;
    public subtitlesError: string;
    public serverErrors: Array<string>;
    public formSubmited = false;

    constructor(private createMaterialService: CreateMaterialService) {
        this.material = new MaterialModel();
        this.imagePreview = "";
        this.imageError = "";
        this.subtitlesError = "";
        this.serverErrors = new Array<string>();
    }

    private validateImage(image: File): boolean {
        let error: string;
        if (!image) {
            this.imageError = "";
            return false;
        }
        if (image.type !== 'image/jpeg' && image.type !== 'image/png') {
            this.imageError = "Invalid image format";
            return false;
        }
        if (image.size > 2000000) {
            this.imageError = "The image is too large";
            return false;
        }
        this.imageError = "";
        return true;
    }

    private validateSubtitles(subtitles: File): boolean {
        let error: string;
        if (!subtitles) {
            this.imageError = "";
            return false;
        }
        if (subtitles.size > 30000000) {
            this.subtitlesError = "The subtitles file is too large";
            return false;
        }
        this.subtitlesError = "";
        return true;
    }


    public formSubmit(form: NgForm): void {
        /* empty logic */
    }

    public imageInserted(event): void {
        let image: File = <File>event.srcElement.files[0];
        if (this.validateImage(image)) {
            this.material.image = image;
            let reader = new FileReader();
            reader.onload = (e) => {
                this.imagePreview = (<any>e.target).result;
            }
            reader.readAsDataURL(image);
        } else {
            this.imagePreview = "";
            event.target.value = "";
        }
    }

    public subtitlesInserted(event): void {
        let subtitles: File = <File>event.srcElement.files[0];
        if (this.validateSubtitles(subtitles)) {
            this.createMaterialService.parseSubtitles(subtitles).subscribe(
                response => {
                    if (response.succeeded) {
                        this.material.words = response.words.map((w) => { let word = new Word(); word.theWord = w.theWord; word.count = w.count; return word; });
                        this.serverErrors = new Array<string>();
                    } else {
                        this.serverErrors = response.errors;
                        this.clearWords();
                    }
                },
                err => {
                    this.serverErrors = new Array<string>("Connection error");
                    this.clearWords();
                }
            );
        } else {
            event.target.value = "";
            this.clearWords();
        }
    }

    private clearWords(): void {
        this.material.words = new Array<Word>();
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
                    if (response.succeeded) {
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