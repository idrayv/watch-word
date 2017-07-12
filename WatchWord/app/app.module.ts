import { NgModule } from '@angular/core';
import { HttpModule, Http } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { AuthHttpService, AuthService } from './auth/auth.service'
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { UserService } from './auth/user.service';
import { SpinnerService } from './global/spinner/spinner.service';
import { ModalService } from './global/components/modal/modal.service';
import { TranslationModalService } from './global/components/translation-modal/translation-modal.service';
import { TranslationService } from './global/components/translation-modal/translation.service';

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
        ModalService,
        TranslationModalService,
        TranslationService,
        SpinnerService,
        UserService,
        AuthService,
        { provide: Http, useClass: AuthHttpService }
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }