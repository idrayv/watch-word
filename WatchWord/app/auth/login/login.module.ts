import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component'
import { LoginRoutingModule } from './login-routing.module'
import { FormsModule } from '@angular/forms';
import { AuthService } from "../auth.service";
import { HttpModule } from "@angular/http";

@NgModule({
    imports: [CommonModule, LoginRoutingModule, FormsModule, HttpModule],
    declarations: [LoginComponent],
    providers: [AuthService]
})

export class LoginModule { }