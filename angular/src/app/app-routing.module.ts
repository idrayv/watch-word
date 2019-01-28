import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { UsersComponent } from './users/users.component';
import { RolesComponent } from '@app/roles/roles.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: AppComponent,
        children: [
          {
            path: 'users',
            component: UsersComponent,
            data: {permission: 'Pages.Users'},
            canActivate: [AppRouteGuard]
          },
          {
            path: 'roles',
            component: RolesComponent,
            data: {permission: 'Pages.Roles'},
            canActivate: [AppRouteGuard]
          },
          {
            path: 'material',
            loadChildren: 'app/material/material.module#MaterialModule',
            data: {preload: true}
          },
          {
            path: 'materials',
            loadChildren: 'app/materials/materials.module#MaterialsModule',
            data: {preload: true}
          },
          {
            path: 'dictionaries',
            loadChildren: 'app/dictionaries/dictionaries.module#DictionariesModule',
            data: {preload: true, permission: 'Member'},
            canActivate: [AppRouteGuard]
          },
          {
            path: 'cards',
            loadChildren: 'app/games/cards/cards.module#CardsModule',
            data: {preload: true, permission: 'Member'},
            canActivate: [AppRouteGuard]
          }
        ]
      }
    ])
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
