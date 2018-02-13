import {Router} from '@angular/router';
import {Component, Injector} from '@angular/core';
import {NgForm, NgModel} from '@angular/forms';
import {AuthService} from '../auth.service';
import {AccountInformationService} from '../account-information.service';
import {LoginModel, Account, AccountInformation} from '../auth.models';
import {ComponentValidation} from '../../global/component-validation';
import {AppComponentBase} from '@shared/app-component-base';

@Component({
    templateUrl: 'login.template.html'
})

export class LoginComponent extends AppComponentBase {
    public model: LoginModel = new LoginModel();
    public formSubmitted = false;

    constructor(private auth: AuthService,
                private accountInformationService: AccountInformationService,
                private router: Router,
                injector: Injector) {
        super(injector);
    }

    public logIn(form: NgForm): void {
        this.formSubmitted = true;
        if (form.valid) {
            abp.ui.setBusy('body');
            this.auth.authenticate(this.model).then(response => {
                abp.ui.clearBusy('body');
                if (response.success) {
                    this.accountInformationService.setAccountInformation(
                        new AccountInformation(
                            new Account(response.account.externalId, response.account.name), response.isAdmin)
                    );
                    this.router.navigate(['home']);
                } else {
                    response.errors.forEach((err) => abp.notify.error(err));
                }
            });
            this.formSubmitted = false;
            form.reset();
        }
    }

    public validationErrors(state: NgModel): string[] {
        return ComponentValidation.validationErrors(state);
    }
}
