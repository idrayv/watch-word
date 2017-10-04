import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { AuthService } from '../auth.service';
import { AccountService } from '../account.service';
import { LoginModel, Account } from '../auth.models';
import { SpinnerService } from '../../global/spinner/spinner.service';
import { ComponentValidation } from '../../global/component-validation';
import { BaseComponent } from '../../global/base-component';

@Component({
    templateUrl: 'login.template.html'
})

export class LoginComponent extends BaseComponent {
    public model: LoginModel = new LoginModel();
    public formSubmited = false;

    constructor(private auth: AuthService, private accountService: AccountService, private router: Router,
        private spinner: SpinnerService) {
        super();
    }

    public logIn(form: NgForm): void {
        this.formSubmited = true;
        if (form.valid) {
            const login = this.model.login;
            this.spinner.displaySpinner(true);
            this.auth.authenticate(this.model).then(response => {
                this.spinner.displaySpinner(false);
                if (response.success) {
                    this.accountService.setAccount(new Account(response.account.externalId, response.account.name));
                    this.router.navigate(['home']);
                } else {
                    response.errors.forEach((err) => this.displayError(err, 'LogIn error'));
                }
            });
            this.formSubmited = false;
            form.reset();
        }
    }

    public validationErrors(state: NgModel): string[] {
        return ComponentValidation.validationErrors(state);
    }
}
