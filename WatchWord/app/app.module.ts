import { NgModule, Injector } from '@angular/core';
import { HttpModule, Http } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { AuthHttpService, AuthService } from './auth/auth.service'
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { UserService } from './auth/user.service';
import { SpinnerService } from './global/spinner/spinner.service';
import { MaterialsSearchComponent } from './materials-search/materials-search.component';
import { FormsModule } from '@angular/forms/';
import { MaterialsSearchService } from './materials-search/materials-search.service';
import { ToastModule } from 'ng2-toastr/ng2-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastOptions } from 'ng2-toastr/src/toast-options';
import { ServiceLocator } from './global/service-locator';
import { ToastService } from './global/toast/toast.service';
import { CustomOption } from './global/toast/toast.models';

@NgModule({
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpModule,
        AppRoutingModule,
        FormsModule,
        ToastModule.forRoot()
    ],
    declarations: [
        AppComponent,
        MaterialsSearchComponent
    ],
    providers: [
        SpinnerService,
        UserService,
        AuthService,
        { provide: Http, useClass: AuthHttpService },
        MaterialsSearchService,
        { provide: ToastOptions, useClass: CustomOption },
        ToastService
    ],
    bootstrap: [AppComponent]
})

export class AppModule {
    constructor(private injector: Injector) {
        ServiceLocator.Injector = this.injector;
    }
}