import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth.service';
import { LoginComponent } from './login.component';
import { LoginRoutingModule } from './login-routing.module';

@NgModule({
    imports: [CommonModule, LoginRoutingModule, FormsModule, HttpModule],
    declarations: [LoginComponent],
    providers: [AuthService]
})

export class LoginModule {
}