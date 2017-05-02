import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component'
import { LoginRoutingModule } from './login-routing.module'
import { FormsModule } from '@angular/forms';

@NgModule({
    imports: [CommonModule, LoginRoutingModule, FormsModule],
    declarations: [LoginComponent],
    providers: []
})

export class LoginModule { }