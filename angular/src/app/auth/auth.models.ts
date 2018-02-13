import {BaseResponseModel} from '../global/models';

export class LoginModel {
    public login: string;
    public password: string;
}

export class RegisterModel {
    public login: string;
    public email: string;
    public password: string;
}

export class Account {
    constructor(externalId: number, name: string) {
        this.externalId = externalId;
        this.name = name;
    }

    public externalId: number;
    public name: string;
}

export class AccountInformation {
    constructor(account: Account, isAdmin: boolean = false) {
        this.account = account;
        this.isAdmin = isAdmin;
    }

    public account: Account;
    public isAdmin: boolean;
}

export class AuthResponseModel extends BaseResponseModel {
    public account: Account;
    public isAdmin: boolean;
}
