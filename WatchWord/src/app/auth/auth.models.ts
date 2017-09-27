export class LoginModel {
    public login: string;
    public password: string;
}

export class RegisterModel {
    public login: string;
    public email: string;
    public password: string;
}

export class UserModel {
    constructor(name: string, isLoggedIn: boolean) {
        this.name = name;
        this.isLoggedIn = isLoggedIn;
    }

    public name: string;
    public isLoggedIn: boolean;
}
