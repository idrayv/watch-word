import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register.component'
import { RegisterRoutingModule } from './register-routing.module'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from "../auth.service";
import { HttpModule } from "@angular/http";

@NgModule({
    imports: [CommonModule, RegisterRoutingModule, FormsModule, ReactiveFormsModule, HttpModule],
    declarations: [RegisterComponent],
    providers: [AuthService]
})

export class RegisterModule { }