import { Injectable } from '@angular/core';
import { ChangePasswordComponent } from '@app/layout/profile/change-password/change-password.component';

@Injectable()
export class ChangePasswordService {

  private changePasswordModal: ChangePasswordComponent;

  constructor() {
  }

  public setPasswordComponent(changePasswordModal: ChangePasswordComponent): void {
    this.changePasswordModal = changePasswordModal;
  }

  public getPasswordComponent(): ChangePasswordComponent {
    return this.changePasswordModal;
  }
}
