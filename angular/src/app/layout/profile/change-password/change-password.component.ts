import {Component, ElementRef, Injector, ViewChild, ViewEncapsulation} from '@angular/core';
import {AppComponentBase} from '@shared/app-component-base';
import {ModalDirective} from 'ngx-bootstrap';
import {AccountServiceProxy, ChangePasswordInput} from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'ww-change-password',
    templateUrl: './change-password.component.html',
    styleUrls: ['./change-password.component.less'], // TODO: Fix styles to be like user edit modals
    encapsulation: ViewEncapsulation.None
})
export class ChangePasswordComponent extends AppComponentBase {

    @ViewChild('currentPasswordInput') currentPasswordInput: ElementRef;
    @ViewChild('changePasswordModal') modal: ModalDirective;

    currentPassword: string;
    password: string;
    confirmPassword: string;
    saving = false;
    active = false;

    constructor(injector: Injector,
                private accountService: AccountServiceProxy) {
        super(injector);
    }

    show(): void {
        this.active = true;
        this.currentPassword = '';
        this.password = '';
        this.confirmPassword = '';
        this.modal.show();
    }

    onShown(): void {
        $(this.currentPasswordInput.nativeElement).focus();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    save(): void {
        const input = new ChangePasswordInput();
        input.currentPassword = this.currentPassword;
        input.newPassword = this.password;
        this.saving = true;
        this.accountService.changePassword(input).finally(() => {
            this.saving = false;
        }).subscribe(() => {
            this.notify.info(this.l('Your password has changed successfully'));
            this.close();
        });
    }
}
