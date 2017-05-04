import { Component } from '@angular/core';
import { MaterialModel } from "./material.model";
import { MaterialType } from "./material.model";
import { NgForm } from "@angular/forms";

@Component({
    templateUrl: "app/create-material/create-material.template.html"
})

export class CreateMaterialComponent {
    public material: MaterialModel;

    constructor() {
        this.material = new MaterialModel();
    }

    public formSubmit(form: NgForm): void {

    }

    public imageInserted(event): void {
        let image: File = <File>event.srcElement.files[0];
        this.material.image = image;
    }

    public subtitlesInserted(event): void {
        let subtitles: File = <File>event.srcElement.files[0];
        this.material.subtitles = subtitles;
    }
}