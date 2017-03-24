import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DictionariesComponent } from './dictionaries.component'
import { DictionariesRoutingModule } from './dictionaries-routing.module'

@NgModule({
    imports: [CommonModule, DictionariesRoutingModule],
    declarations: [DictionariesComponent],
    providers: []
})

export class DictionariesModule { }