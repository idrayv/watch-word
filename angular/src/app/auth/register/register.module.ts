import {NgModule} from '@angular/core';
import {HttpModule} from '@angular/http';
import {FormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {AuthService} from '../auth.service';
import {RegisterComponent} from './register.component';
import {RegisterRoutingModule} from './register-routing.module';

@NgModule({
    imports: [CommonModule, RegisterRoutingModule, FormsModule, HttpModule],
    declarations: [RegisterComponent],
    providers: [AuthService]
})

export class RegisterModule {
}
