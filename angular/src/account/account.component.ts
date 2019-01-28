import { Component, OnInit, ViewEncapsulation, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  templateUrl: './account.component.html',
  styleUrls: [
    './account.component.less'
  ],
  encapsulation: ViewEncapsulation.None
})
export class AccountComponent extends AppComponentBase implements OnInit {

  versionText: string;
  currentYear: number;

  public constructor(injector: Injector) {
    super(injector);

    this.currentYear = new Date().getFullYear();
    this.versionText = this.appSession.application.version
      + ' [' + this.appSession.application.releaseDate.format('YYYYDDMM') + ']';
  }

  ngOnInit(): void {
    $('body').attr('class', 'login-page');
  }
}
