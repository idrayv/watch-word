import {Component, Injector, ElementRef, ViewChild, AfterViewInit} from '@angular/core';
import {AppComponentBase} from '@shared/app-component-base';
import {LoginService} from './login.service';
import {accountModuleAnimation} from '@shared/animations/routerTransition';

@Component({
    templateUrl: './login.component.html',
    styleUrls: [
        './login.component.less'
    ],
    animations: [accountModuleAnimation()]
})
export class LoginComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('cardBody') cardBody: ElementRef;
    submitting = false;

    constructor(injector: Injector,
                public loginService: LoginService) {
        super(injector);
    }

    ngAfterViewInit(): void {
        $(this.cardBody.nativeElement).find('input:first').focus();
    }

    get isSelfRegistrationAllowed(): boolean {
        return true;
    }

    login(): void {
        this.submitting = true;
        abp.ui.setBusy($('.body'));
        this.loginService.authenticate(
            () => {
                abp.ui.clearBusy($('.body'));
                this.submitting = false;
            }
        );
    }
}
