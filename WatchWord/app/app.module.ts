import { NgModule } from '@angular/core';
import { HttpModule, Http } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { AuthHttpService, AuthService } from './auth/auth.service'
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { UserService } from './auth/user.service';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        AppRoutingModule
    ],
    declarations: [
        AppComponent
    ],
    providers: [
        UserService,
        AuthService,
        { provide: Http, useClass: AuthHttpService }
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }