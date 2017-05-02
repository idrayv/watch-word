import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateMaterialComponent } from './create-material.component'
import { CreateMaterialRoutingModule } from './create-material-routing.module'
import { FormsModule } from '@angular/forms';

@NgModule({
    imports: [CommonModule, CreateMaterialRoutingModule, FormsModule],
    declarations: [CreateMaterialComponent],
    providers: []
})

export class CreateMaterialModule { }