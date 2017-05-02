import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register.component'
import { RegisterRoutingModule } from './register-routing.module'
import { FormsModule } from '@angular/forms';

@NgModule({
    imports: [CommonModule, RegisterRoutingModule, FormsModule],
    declarations: [RegisterComponent],
    providers: []
})

export class RegisterModule { }