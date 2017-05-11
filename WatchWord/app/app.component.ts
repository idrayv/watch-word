import { Component, OnDestroy } from '@angular/core';
import { UserService } from './auth/user.service';
import { AuthService } from './auth/auth.service';
import { UserModel } from './auth/auth.models';
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'watch-word',
    templateUrl: "app/app.template.html"
})

export class AppComponent implements OnDestroy {
    userModel: UserModel;
    subscription: Subscription;

    constructor(private userService: UserService, private authService: AuthService) {
        this.userModel = new UserModel('', false);
        this.subscription = userService.getUserObservable().subscribe(user => {
            this.userModel = user;
        });
        userService.initializeUser();
    }

    public logOut() {
        this.authService.logout().subscribe(
            response => {
                if (response.succeeded) {
                    this.userService.setUser(new UserModel('', false));
                } else {
                    console.log(response.errors);
                }
            },
            err => {
                console.log("Logout error occured!");
            });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}