import { Injectable } from "@angular/core";
import { NgModel } from "@angular/forms";

export class ComponentValidation {
    private capitalizeFirstLetter(word: string): string {
        return word.charAt(0).toUpperCase() + word.slice(1);
    }

    public validationErrors(state: NgModel): Array<string> {
        let errors: Array<string> = new Array<string>();
        let name = this.capitalizeFirstLetter(state.name);
        if (state.invalid) {
            for (var error in state.errors) {
                switch (error) {
                    case "minlength":
                        errors.push(`${name} must be at least ${state.errors[error].requiredLength} characters!`);
                        break;
                    case "required":
                        errors.push(`${name} must be filled in!`);
                        break;
                    case "subtitlesInput":
                        (<Array<string>>state.errors[error]).forEach((er) => errors.push(er));
                        break;
                    case "imageInput":
                        (<Array<string>>state.errors[error]).forEach((er) => errors.push(er));
                        break;
                }
            }
        }
        return errors;
    }
}