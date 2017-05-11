import { Component } from '@angular/core';
import { NgForm } from "@angular/forms";
import { RegisterFormGroup } from "./register-form.model";
import { RegisterModel, UserModel } from "../auth.models";
import { AuthService } from "../auth.service";
import { UserService } from "../user.service";
import { Router } from '@angular/router';

@Component({
    templateUrl: "app/auth/register/register.template.html"
})

export class RegisterComponent {
    model: RegisterModel;
    form: RegisterFormGroup;
    formSubmitted: boolean;
    registrationErrors: Array<string>;

    constructor(private auth: AuthService, private userService: UserService, private router: Router) {
        this.model = new RegisterModel();
        this.form = new RegisterFormGroup();
        this.formSubmitted = false;
        this.registrationErrors = new Array<string>();
    }

    submit(form: NgForm) {
        this.formSubmitted = true;
        if (form.valid) {
            let login = this.model.login;
            this.auth.register(this.model).subscribe(
                response => {
                    if (response.succeeded) {
                        this.userService.setUser(new UserModel(login, true));
                        this.router.navigate(['home']);
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