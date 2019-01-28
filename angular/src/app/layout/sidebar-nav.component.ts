import { Component, Injector, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';

@Component({
  templateUrl: './sidebar-nav.component.html',
  selector: 'ww-sidebar-nav',
  encapsulation: ViewEncapsulation.None
})
export class SideBarNavComponent extends AppComponentBase {

  menuItems: MenuItem[] = [
    new MenuItem(this.l('Create material'), 'Member', 'add', '/app/material'),
    new MenuItem(this.l('Materials'), '', 'local_movies', '/app/materials'),
    new MenuItem(this.l('Dictionaries'), 'Member', 'school', '/app/dictionaries'),
    new MenuItem(this.l('Games'), '', 'extension', '', [
      new MenuItem(this.l('Flashcards'), 'Member', 'bookmarks', '/app/cards'),
    ]),
    new MenuItem(this.l('Users'), 'Pages.Users', 'person', '/app/users'),
    new MenuItem(this.l('Roles'), 'Pages.Roles', 'group', '/app/roles')
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
