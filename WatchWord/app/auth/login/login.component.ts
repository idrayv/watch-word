import { Component } from '@angular/core';
import { NgForm } from "@angular/forms";
import { LoginModel } from "../auth.models";
import { AuthService } from "../auth.service";

@Component({
    templateUrl: "app/auth/login/login.template.html"
})

export class LoginComponent {
    public model: LoginModel;

    constructor(private auth: AuthService) {
        this.model = new LoginModel();
    }

    public login(form: NgForm): void {
        if (form.valid) {
            this.auth.authenticate(this.model).subscribe(
                response => {
                    if (response.succeeded) {
                        console.log("auth ok");
                    } else {
                        response.errors.forEach(error => {
                            console.log(error);
                        });
                    }
                },
                err => {
                    console.log("auth error");
                }
            );
        }
    }
}