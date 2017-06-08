import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateMaterialComponent } from './create-material.component'
import { CreateMaterialRoutingModule } from './create-material-routing.module'
import { FormsModule } from '@angular/forms';
import { CreateMaterialService } from './create-material.service';
import { FileInputComponent } from '../components/file-input/file-input.component';
import { FileTypeValidator } from '../components/file-input/file-type-validation.directive';
import { SubtitlesInputComponent } from '../components/subtitles-input/subtitles-input.component';


@NgModule({
    imports: [CommonModule, CreateMaterialRoutingModule, FormsModule],
    declarations: [CreateMaterialComponent, FileInputComponent, FileTypeValidator, SubtitlesInputComponent],
    providers: [CreateMaterialService]
})


export class CreateMaterialModule { }