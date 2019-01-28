import { Component, Injector, ElementRef, AfterViewInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AccountServiceProxy, RegisterInput, RegisterOutput } from '@shared/service-proxies/service-proxies'
import { AppComponentBase } from '@shared/app-component-base';
import { LoginService } from '../login/login.service';
import { accountModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  templateUrl: './register.component.html',
  animations: [accountModuleAnimation()]
})
export class RegisterComponent extends AppComponentBase implements AfterViewInit {

  @ViewChild('cardBody') cardBody: ElementRef;

  model: RegisterInput = new RegisterInput();
  saving = false;

  constructor(injector: Injector,
              private _accountService: AccountServiceProxy,
              private _router: Router,
              private readonly _loginService: LoginService) {
    super(injector);
  }

  ngAfterViewInit(): void {
    $(this.cardBody.nativeElement).find('input:first').focus();
  }ß

  save(): void {
    this.saving = true;
    abp.ui.setBusy($('.body'));
    this._accountService.register(this.model)
      .finally(() => {
        abp.ui.clearBusy($('.body'));
        this.saving = false;
      })
      .subscribe((result: RegisterOutput) => {
        if (!result.canLogin) {
          this.notify.success(this.l('SuccessfullyRegistered'));
          this._router.navigate(['/login']);
          return;
        }

        // Authenticate
        this.saving = true;
        abp.ui.setBusy($('.body'));
        this._loginService.authenticateModel.userNameOrEmailAddress = this.model.userName;
        this._loginService.authenticateModel.password = this.model.password;
        this._loginService.authenticate(() => {
          abp.ui.clearBusy($('.body'));
          this.saving = false;
        });
      });
  }
}
