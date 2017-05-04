import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register.component'
import { RegisterRoutingModule } from './register-routing.module'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [CommonModule, RegisterRoutingModule, FormsModule, ReactiveFormsModule],
    declarations: [RegisterComponent],
    providers: []
})

export class RegisterModule { }