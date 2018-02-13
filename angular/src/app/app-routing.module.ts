import {NgModule} from '@angular/core';
import {RouterModule} from '@angular/router';
import {AppComponent} from './app.component';
import {AppRouteGuard} from '@shared/auth/auth-route-guard';
import {UsersComponent} from './users/users.component';
import {RolesComponent} from '@app/roles/roles.component';

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
                        path: 'materials',
                        loadChildren: 'app/materials/materials.module#MaterialsModule', // Lazy load account module
                        data: {preload: true}
                    }
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule {

}
