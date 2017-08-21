import{ Component, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { UserService } from './auth/user.service';
import { AuthService } from './auth/auth.service';
import { UserModel } from './auth/auth.models';
import { SpinnerService } from './global/spinner/spinner.service';
import { ToastService } from './global/toast/toast.service';
import { ToastsManager } from 'ng2-toastr/src/toast-manager';
import { ToastModel, ToastType } from './global/toast/toast.models';

@Component({
    selector: 'watch-word',
    templateUrl: 'app/app.template.html'
})

export class AppComponent implements OnDestroy, OnInit {
    public spinnerStatus: boolean;
    public userModel: UserModel;
    private userSubscription: Subscription;
    private spinnerSubscription: Subscription;
    private toastSubscription: Subscription;

    constructor(private userService: UserService, private authService: AuthService, private spinner: SpinnerService,
        private toast: ToastService, private toastr: ToastsManager, private vcr: ViewContainerRef) {
    }

    ngOnInit() {
        // toast's
        this.toastr.setRootViewContainerRef(this.vcr);
        this.toastSubscription = this.toast.getObservable().subscribe(value => this.showToast(value));

        // auth
        this.userService.initializeUser();
        this.userSubscription = this.userService.getUserObservable().subscribe(user => {
            this.userModel = user;
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
                this.userService.setUser(new UserModel('', false));
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
        this.userSubscription.unsubscribe();
        this.spinnerSubscription.unsubscribe();
        this.toastSubscription.unsubscribe();
    }
}