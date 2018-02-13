import {NgForm, NgModel} from '@angular/forms';
import {Router} from '@angular/router';
import {Component, Injector} from '@angular/core';
import {AuthService} from '../auth.service';
import {AccountInformationService} from '../account-information.service';
import {RegisterModel, Account, AccountInformation} from '../auth.models';
import {ComponentValidation} from '../../global/component-validation';
import {AppComponentBase} from '@shared/app-component-base';

@Component({
    templateUrl: 'register.template.html'
})

export class RegisterComponent extends AppComponentBase {
    public model: RegisterModel = new RegisterModel();
    public formSubmitted = false;

    constructor(private auth: AuthService,
                private accountInformationService: AccountInformationService,
                private router: Router,
                injector: Injector) {
        super(injector);
    }

    public register(form: NgForm) {
        this.formSubmitted = true;
        if (form.valid) {
            abp.ui.setBusy('body');
            this.auth.register(this.model).then(response => {
                abp.ui.clearBusy('body');
                if (response.success) {
                    this.accountInformationService.setAccountInformation(
                        new AccountInformation(new Account(response.account.externalId, response.account.name), response.isAdmin)
                    );
                    this.router.navigate(['home']);
                } else {
                    response.errors.forEach((err) => this.displayError(err));
                }
            });
            form.reset();
            this.formSubmitted = false;
        }
    }

    public validationErrors(state: NgModel): string[] {
        return ComponentValidation.validationErrors(state);
    }
}
