import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { UserService } from './auth/user.service';
import { AuthService } from './auth/auth.service';
import { UserModel } from './auth/auth.models';
declare var $: any;

@Component({
    selector: 'watch-word',
    templateUrl: 'app/app.template.html'
})

export class AppComponent implements OnDestroy, OnInit {
    userModel: UserModel;
    subscription: Subscription;

    constructor(private userService: UserService, private authService: AuthService) { }

    ngOnInit() {
        this.userModel = new UserModel('', false);
        this.subscription = this.userService.getUserObservable().subscribe(user => {
            this.userModel = user;
        });
        this.userService.initializeUser();
    }

    public logOut() {
        this.authService.logout().then(
            response => {
                if (response.success) {
                    this.userService.setUser(new UserModel('', false));
                } else {
                    console.log(response.errors);
                }
            }
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}