﻿import {Component, Injector, ViewEncapsulation} from '@angular/core';
import {AppComponentBase} from '@shared/app-component-base';
import {MenuItem} from '@shared/layout/menu-item';

@Component({
    templateUrl: './sidebar-nav.component.html',
    selector: 'ww-sidebar-nav',
    encapsulation: ViewEncapsulation.None
})
export class SideBarNavComponent extends AppComponentBase {

    menuItems: MenuItem[] = [
        new MenuItem(this.l('Create material'), '', 'local_offer', '/app/material'),
        new MenuItem(this.l('Materials'), '', 'local_offer', '/app/materials'),
        new MenuItem(this.l('Users'), 'Pages.Users', 'people', '/app/users'),
        new MenuItem(this.l('Roles'), 'Pages.Roles', 'local_offer', '/app/roles')
    ];

    constructor(injector: Injector) {
        super(injector);
    }

    showMenuItem(menuItem): boolean {
        if (menuItem.permissionName) {
            return this.permission.isGranted(menuItem.permissionName);
        }

        return true;
    }
}
