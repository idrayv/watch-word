import { Component } from '@angular/core';
import { MaterialType, MaterialModel, ParseResponseModel } from "./material.models";
import { NgForm } from "@angular/forms";
import { CreateMaterialService } from "./create-material.service";

@Component({
    templateUrl: "app/create-material/create-material.template.html"
})

export class CreateMaterialComponent {
    public material: MaterialModel;

    constructor(private createMaterialService: CreateMaterialService) {
        this.material = new MaterialModel();
    }

    public formSubmit(form: NgForm): void {
        this.createMaterialService.parseSubtitles(this.material.subtitles).subscribe(
            response => {
                if (response.succeeded) {
                    console.log(response.words);
                } else {
                    response.errors.forEach(error => {
                        console.log(error);
                    });
                }
            },
            err => {
                console.log("parse error");
            }
        );
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