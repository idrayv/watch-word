import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialComponent } from './material.component'
import { MaterialRoutingModule } from './material-routing.module'
import { FormsModule } from '@angular/forms';
import { MaterialService } from './material.service';
import { ComponentValidationService } from '../services/component-validation.service';
import { ImageInputComponent } from '../components/image-input/image-input.component';
import { FileTypeValidator } from '../directives/file-type-validation.directive';
import { SubtitlesInputComponent } from '../components/subtitles-input/subtitles-input.component';

@NgModule({
    imports: [CommonModule, MaterialRoutingModule, FormsModule],
    declarations: [MaterialComponent, ImageInputComponent, FileTypeValidator, SubtitlesInputComponent],
    providers: [MaterialService, ComponentValidationService]
})

export class MaterialModule { }