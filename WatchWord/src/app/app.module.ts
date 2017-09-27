import { NgModule, Injector } from '@angular/core';
import { FormsModule } from '@angular/forms/';
import { RouterModule, Routes } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastModule } from 'ng2-toastr/ng2-toastr';
import { ToastOptions } from 'ng2-toastr/src/toast-options';
import { SpinnerService } from './global/spinner/spinner.service';
import { ServiceLocator } from './global/service-locator';
import { ToastService } from './global/toast/toast.service';
import { CustomOption } from './global/toast/toast.models';
import { HttpClientInterceptor } from './global/http-interceptor';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { UserService } from './auth/user.service';
import { AuthService } from './auth/auth.service';
import { MaterialsSearchComponent } from './materials-search/materials-search.component';
import { MaterialsSearchService } from './materials-search/materials-search.service';

@NgModule({
    imports: [
        BrowserModule,
        HttpClientModule,
        BrowserAnimationsModule,
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
        { provide: HTTP_INTERCEPTORS, useClass: HttpClientInterceptor, multi: true },
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
