import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { UserService } from '../user.service';
import { RegisterFormGroup } from './register-form.model';
import { RegisterModel, UserModel } from '../auth.models';

@Component({
    templateUrl: 'app/auth/register/register.template.html'
})

export class RegisterComponent {
    public model: RegisterModel = new RegisterModel();
    public form: RegisterFormGroup = new RegisterFormGroup();
    public formSubmitted: boolean = false;
    public registrationErrors: Array<string> = new Array<string>();

    constructor(private auth: AuthService, private userService: UserService, private router: Router) { }

    submit(form: NgForm) {
        this.formSubmitted = true;
        if (form.valid) {
            let login = this.model.login;
            this.auth.register(this.model).subscribe(
                response => {
                    if (response.success) {
                        this.userService.setUser(new UserModel(login, true));
                        this.router.navigate(['home']);
                    } else {
                        this.registrationErrors = response.errors;
                    }
                },
                err => {
                    this.registrationErrors.push('Registration error occured!');
                }
            );
            form.reset();
            this.formSubmitted = false;
        }
    }
}