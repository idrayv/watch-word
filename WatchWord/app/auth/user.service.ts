import { Injectable } from '@angular/core';
import { UserModel } from './auth.models';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs';

@Injectable()
export class UserService {
    private userModelSubject: Subject<UserModel> = new Subject<UserModel>();

    constructor() { }

    public initializeUser() {
        let name = localStorage.getItem('UserName');
        let userModel = new UserModel(name ? name : '', localStorage.getItem('UserIsLoggedIn') === 'true');

        this.userModelSubject.next(userModel);
    }

    public setUser(user: UserModel) {
        this.userModelSubject.next(user);
        localStorage.setItem('UserName', user.name);
        localStorage.setItem('UserIsLoggedIn', '' + user.isLoggedIn);
    }

    public getUserObservable(): Observable<UserModel> {
        return this.userModelSubject.asObservable();
    }
}