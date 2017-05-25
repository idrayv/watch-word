import { Component } from '@angular/core';
import { NgForm, NgModel } from "@angular/forms";
import { LoginModel, UserModel } from "../auth.models";
import { AuthService } from "../auth.service";
import { UserService } from "../user.service";
import { Router } from '@angular/router';

@Component({
    templateUrl: "app/auth/login/login.template.html"
})

export class LoginComponent {
    public model: LoginModel;

    public formSubmited: boolean;

    public authenticationErrors: Array<string>

    constructor(private auth: AuthService, private userService: UserService, private router: Router) {
        this.model = new LoginModel();
        this.formSubmited = false;
        this.authenticationErrors = new Array<string>();
    }

    public logIn(form: NgForm): void {
        this.formSubmited = true;
        if (form.valid) {
            let login = this.model.login;
            this.auth.authenticate(this.model).subscribe(
                response => {
                    if (response.success) {
                        this.userService.setUser(new UserModel(login, true));
                        this.router.navigate(['home']);
                    } else {
                        this.authenticationErrors = response.errors;
                    }
                },
                err => {
                    this.authenticationErrors.push("Authentification error occured!");
                }
            );
            this.formSubmited = false;
            form.reset();
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
                }
            }
        }
        return errors;
    }
}