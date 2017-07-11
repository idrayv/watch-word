import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { UserService } from '../user.service';
import { RegisterFormGroup } from './register-form.model';
import { RegisterModel, UserModel } from '../auth.models';
import { SpinnerService } from "../../global/spinner/spinner.service";

@Component({
    templateUrl: 'app/auth/register/register.template.html'
})

export class RegisterComponent {
    public model: RegisterModel = new RegisterModel();
    public form: RegisterFormGroup = new RegisterFormGroup();
    public formSubmitted: boolean = false;
    public registrationErrors: Array<string> = new Array<string>();

    constructor(private auth: AuthService, private userService: UserService, private router: Router, private spinner: SpinnerService) { }

    submit(form: NgForm) {
        this.formSubmitted = true;
        if (form.valid) {
            let login = this.model.login;
            this.spinner.displaySpinner(true);
            this.auth.register(this.model).then(
                response => {
                    this.spinner.displaySpinner(false);
                    if (response.success) {
                        this.userService.setUser(new UserModel(login, true));
                        this.router.navigate(['home']);
                    } else {
                        this.registrationErrors = response.errors;
                    }
                }
            );
            form.reset();
            this.formSubmitted = false;
        }
    }
}