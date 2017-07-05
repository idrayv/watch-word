import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { AuthService } from '../auth.service';
import { UserService } from '../user.service';
import { LoginModel, UserModel } from '../auth.models';
import { ComponentValidation } from '../../abstract/component-validation';
import { SpinnerService } from '../../spinner/spinner.service';

@Component({
    templateUrl: 'app/auth/login/login.template.html'
})

export class LoginComponent extends ComponentValidation {
    public model: LoginModel = new LoginModel();
    public formSubmited: boolean = false;
    public authenticationErrors: Array<string> = new Array<string>();

    constructor(private auth: AuthService, private userService: UserService, private router: Router, private spinner: SpinnerService) {
        super();
    }

    public logIn(form: NgForm): void {
        this.formSubmited = true;
        if (form.valid) {
            let login = this.model.login;
            this.spinner.displaySpinner(true);
            this.auth.authenticate(this.model).then(
                response => {
                    this.spinner.displaySpinner(false);
                    if (response.success) {
                        this.userService.setUser(new UserModel(login, true));
                        this.router.navigate(['home']);
                    } else {
                        this.authenticationErrors = response.errors;
                    }
                }
            );
            this.formSubmited = false;
            form.reset();
        }
    }
}