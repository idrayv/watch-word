import { Component } from '@angular/core';
import { NgForm } from "@angular/forms";
import { RegisterFormGroup } from "./form.model";
import { RegisterModel } from "./register-model";

@Component({
    templateUrl: "app/identity/register/register.template.html"
})

export class RegisterComponent {
    model: RegisterModel;
    form: RegisterFormGroup;
    formSubmitted: boolean;

    constructor() {
        this.model = new RegisterModel();
        this.form = new RegisterFormGroup();
        this.formSubmitted = false;
    }

    submit(form: NgForm) {
        debugger;
        this.formSubmitted = true;
        if (form.valid) {
            form.reset();
            this.formSubmitted = false;
        }
    }
}