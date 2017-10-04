import { Component, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { AccountService } from './auth/account.service';
import { AuthService } from './auth/auth.service';
import { Account } from './auth/auth.models';
import { SpinnerService } from './global/spinner/spinner.service';
import { ToastService } from './global/toast/toast.service';
import { ToastsManager } from 'ng2-toastr/src/toast-manager';
import { ToastModel, ToastType } from './global/toast/toast.models';

@Component({
    selector: 'ww-app',
    templateUrl: 'app.template.html'
})

export class AppComponent implements OnDestroy, OnInit {
    public spinnerStatus: boolean;
    public account: Account;
    private accountSubscription: Subscription;
    private spinnerSubscription: Subscription;
    private toastSubscription: Subscription;

    constructor(private accountService: AccountService, private authService: AuthService, private spinner: SpinnerService,
        private toast: ToastService, private toastr: ToastsManager, private vcr: ViewContainerRef) {
    }

    ngOnInit() {
        // toast's
        this.toastr.setRootViewContainerRef(this.vcr);
        this.toastSubscription = this.toast.getObservable().subscribe(value => this.showToast(value));

        // auth
        this.accountService.initializeAccount();
        this.accountSubscription = this.accountService.getAccountObservable().subscribe(account => {
            this.account = account;
        });

        // spinner
        this.spinnerSubscription = this.spinner.getSpinnerObservable().subscribe(value => {
            this.spinnerStatus = value;
        });
    }

    public logOut() {
        this.spinner.displaySpinner(true);
        this.authService.logout().then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                this.accountService.setAccount(new Account(0, ''));
            } else {
                console.log(response.errors);
            }
        });
    }

    private showToast(toastModel: ToastModel): void {
        if (toastModel.type === ToastType.Error) {
            this.toastr.error(toastModel.message, toastModel.title, {
                toastLife: toastModel.toastLife
            }).catch(() => console.log('Toast error'));
        } else {
            this.toastr.custom(toastModel.html, null, {
                toastLife: toastModel.toastLife,
                enableHTML: true
            }).catch(() => console.log('Custom toast error'));
        }
    }

    ngOnDestroy() {
        this.accountSubscription.unsubscribe();
        this.spinnerSubscription.unsubscribe();
        this.toastSubscription.unsubscribe();
    }
}
