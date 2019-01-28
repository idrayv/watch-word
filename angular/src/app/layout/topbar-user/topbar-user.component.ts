import { Component, Injector, OnInit, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { Router } from '@angular/router';
import { ChangePasswordComponent } from '@app/layout/profile/change-password/change-password.component';
import { ChangePasswordService } from '@app/layout/profile/change-password/change-password.service';

@Component({
  selector: 'ww-topbar-user',
  encapsulation: ViewEncapsulation.None,
  templateUrl: './topbar-user.component.html',
  styleUrls: ['./topbar-user.component.less']
})
export class TopBarUserComponent extends AppComponentBase implements OnInit {

  changePasswordModal: ChangePasswordComponent;
  public shownLoginName = '';

  constructor(injector: Injector,
              private _authService: AppAuthService,
              private _router: Router,
              private _changePasswordService: ChangePasswordService) {
    super(injector);
  }

  ngOnInit() {
    this.shownLoginName = this.appSession.getShownLoginName();
  }

  login(): void {
    this._router.navigate(['/account/login']);
  }

  changePassword(): void {
    this.changePasswordModal = this._changePasswordService.getPasswordComponent();
    this.changePasswordModal.show();
  }

  logout(): void {
    this._authService.logout();
  }
}
