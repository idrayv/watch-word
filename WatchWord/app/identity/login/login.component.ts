import { Component } from '@angular/core';
import { NgForm } from "@angular/forms";
import { LoginModel } from "./login-model";

@Component({
    templateUrl: "app/identity/login/login.template.html"
})

export class LoginComponent {
    public model: LoginModel;

    constructor() {
        this.model = new LoginModel();
    }

    public login(form: NgForm): void {

    }
}