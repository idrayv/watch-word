import { Component } from '@angular/core';
import { NgForm } from "@angular/forms";
import { RegisterFormGroup } from "./register-form.model";
import { RegisterModel } from "../auth.models";

@Component({
    templateUrl: "app/auth/register/register.template.html"
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
        this.formSubmitted = true;
        if (form.valid) {
            form.reset();
            this.formSubmitted = false;
        }
    }
}