import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { UserService } from './auth/user.service';
import { AuthService } from './auth/auth.service';
import { UserModel } from './auth/auth.models';
import { SpinnerService } from './global/spinner/spinner.service';

@Component({
    selector: 'watch-word',
    templateUrl: 'app/app.template.html'
})

export class AppComponent implements OnDestroy, OnInit {
    public spinnerStatus: boolean;
    public userModel: UserModel;
    private authSubscription: Subscription;
    private spinnerSubscription: Subscription;

    constructor(private userService: UserService, private authService: AuthService, private spinner: SpinnerService) { }

    ngOnInit() {
        // auth
        this.userModel = new UserModel('', false);
        this.authSubscription = this.userService.getUserObservable().subscribe(user => {
            this.userModel = user;
        });
        this.userService.initializeUser();

        // spinner
        this.spinnerSubscription = this.spinner.getSpinnerObservable().subscribe(value => {
            this.spinnerStatus = value;
        });
    }

    public logOut() {
        this.spinner.displaySpinner(true);
        this.authService.logout().then(
            response => {
                this.spinner.displaySpinner(false);
                if (response.success) {
                    this.userService.setUser(new UserModel('', false));
                } else {
                    console.log(response.errors);
                }
            }
        );
    }

    ngOnDestroy() {
        this.authSubscription.unsubscribe();
        this.spinnerSubscription.unsubscribe();
    }
}