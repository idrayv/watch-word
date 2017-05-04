export class AuthResponseModel {
    public succeeded: boolean;
    public errors: string[];
}

export class LoginModel {
    public login: string;
    public password: string;
}

export class RegisterModel {
    public login: string;
    public email: string;
    public password: string;
}