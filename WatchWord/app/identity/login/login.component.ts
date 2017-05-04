import { Component } from '@angular/core';
import { NgForm } from "@angular/forms";
import { LoginModel } from "./login-model";
import { AuthService } from "../auth-service";

@Component({
    templateUrl: "app/identity/login/login.template.html"
})

export class LoginComponent {
    public model: LoginModel;

    constructor(private auth: AuthService) {
        this.model = new LoginModel();
    }

    public login(form: NgForm): void {
        debugger;
        if (form.valid) {
            this.auth.authenticate(this.model.login, this.model.password);
        }
    }
}