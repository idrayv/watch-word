import { Component, Injector, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { ChangePasswordComponent } from '@app/layout/profile/change-password/change-password.component';
import { ChangePasswordService } from '@app/layout/profile/change-password/change-password.service';
// import {SignalRHelper} from '@shared/helpers/SignalRHelper';
// import {SignalRAspNetCoreHelper} from '@shared/helpers/SignalRAspNetCoreHelper';

@Component({
  templateUrl: './app.component.html'
})
export class AppComponent extends AppComponentBase implements OnInit, AfterViewInit {

  @ViewChild('changePasswordModal') changePasswordModal: ChangePasswordComponent;

  constructor(injector: Injector,
              private changePasswordService: ChangePasswordService) {
    super(injector);
  }

  ngOnInit(): void {
    if (this.appSession.application.features['SignalR']) {
      if (this.appSession.application.features['SignalR.AspNetCore']) {
        // SignalRAspNetCoreHelper.initSignalR();
      } else {
        // SignalRHelper.initSignalR();
      }
    }

    abp.event.on('abp.notifications.received', userNotification => {
      abp.notifications.showUiNotifyForUserNotification(userNotification);

      // Desktop notification
      Push.create('AbpZeroTemplate', {
        body: userNotification.notification.data.message,
        icon: abp.appPath + 'assets/app-logo-small.png',
        timeout: 6000,
        onClick: function () {
          window.focus();
          this.close();
        }
      });
    });
  }

  ngAfterViewInit(): void {
    this.changePasswordService.setPasswordComponent(this.changePasswordModal);

    ($ as any).AdminBSB.activateAll();
    ($ as any).AdminBSB.activateDemo();
  }
}
