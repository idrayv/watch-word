import {Component, Injector, OnInit, ViewEncapsulation} from '@angular/core';
import {AppComponentBase} from '@shared/app-component-base';
import {AppAuthService} from '@shared/auth/app-auth.service';
import {Router} from '@angular/router';

@Component({
    selector: 'ww-topbar-user',
    encapsulation: ViewEncapsulation.None,
    templateUrl: './topbar-user.component.html',
    styleUrls: ['./topbar-user.component.less']
})
export class TopBarUserComponent extends AppComponentBase implements OnInit {

    public shownLoginName = '';

    constructor(injector: Injector,
                private _authService: AppAuthService,
                private _router: Router) {
        super(injector);
    }

    ngOnInit() {
        this.shownLoginName = this.appSession.getShownLoginName();
    }

    login(): void {
        this._router.navigate(['/account/login']);
    }

    logout(): void {
        this._authService.logout();
    }
}
