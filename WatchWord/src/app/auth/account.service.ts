import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { Account } from './auth.models';

@Injectable()
export class AccountService {
    private accountModelSubject: ReplaySubject<Account> = new ReplaySubject<Account>(1);

    constructor() { }

    public initializeAccount() {
        const currentAccountStorage = localStorage.getItem('currentAccount');
        if (currentAccountStorage) {
            this.accountModelSubject.next(JSON.parse(currentAccountStorage));
        } else {
            const accountModel: Account = new Account(0, '');
            this.setAccount(accountModel);
        }
    }

    public setAccount(account: Account) {
        this.accountModelSubject.next(account);
        localStorage.setItem('currentAccount', JSON.stringify(account));
    }

    public getAccountObservable(): Observable<Account> {
        return this.accountModelSubject.asObservable();
    }
}
