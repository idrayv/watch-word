import { Component } from '@angular/core';
import { NgForm } from "@angular/forms";
import { RegisterFormGroup } from "./register-form.model";
import { RegisterModel } from "../auth.models";
import { AuthService } from "../auth.service";

@Component({
    templateUrl: "app/auth/register/register.template.html"
})

export class RegisterComponent {
    model: RegisterModel;
    form: RegisterFormGroup;
    formSubmitted: boolean;
    registrationErrors: Array<string>;

    constructor(private auth: AuthService) {
        this.model = new RegisterModel();
        this.form = new RegisterFormGroup();
        this.formSubmitted = false;
        this.registrationErrors = new Array<string>();
    }

    submit(form: NgForm) {
        this.formSubmitted = true;
        if (form.valid) {
            this.auth.register(this.model).subscribe(
                response => {
                    if (response.succeeded) {
                        console.log("registered");
                        // todo redirect
                    } else {
                        this.registrationErrors = response.errors;
                    }
                },
                err => {
                    this.registrationErrors.push("Registration error occured!");
                }
            );
            form.reset();
            this.formSubmitted = false;
        }
    }
}