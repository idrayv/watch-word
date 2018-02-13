import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {ReplaySubject} from 'rxjs/ReplaySubject';
import {Account, AccountInformation} from './auth.models';

@Injectable()
export class AccountInformationService {
    private accountInformationModelSubject: ReplaySubject<AccountInformation> = new ReplaySubject<AccountInformation>(1);

    constructor() {
    }

    public initializeAccountInformation() {
        const currentAccountInformationStorage = localStorage.getItem('currentAccountInformation');
        if (currentAccountInformationStorage) {
            this.accountInformationModelSubject.next(JSON.parse(currentAccountInformationStorage));
        } else {
            const accountInformationModel: AccountInformation = new AccountInformation(new Account(0, ''));
            this.setAccountInformation(accountInformationModel);
        }
    }

    public setAccountInformation(accountInformation: AccountInformation) {
        this.accountInformationModelSubject.next(accountInformation);
        localStorage.setItem('currentAccountInformation', JSON.stringify(accountInformation));
    }

    public getAccountInformationObservable(): Observable<AccountInformation> {
        return this.accountInformationModelSubject.asObservable();
    }
}
