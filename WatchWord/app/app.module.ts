import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DictionariesComponent } from './dictionaries/dictionaries.component';
import { MaterialsComponent } from './materials/materials.component';

const appRoutes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: '', component: HomeComponent },
    { path: 'dictionaries', component: DictionariesComponent },
    { path: 'materials', component: MaterialsComponent }
]

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        RouterModule.forRoot(appRoutes)
    ],
    declarations: [
        AppComponent,
        HomeComponent,
        DictionariesComponent,
        MaterialsComponent
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }