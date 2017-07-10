import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialComponent } from './material.component'
import { MaterialRoutingModule } from './material-routing.module'
import { FormsModule } from '@angular/forms';
import { MaterialService } from './material.service';
import { ImageInputComponent } from './components/image-input/image-input.component';
import { SubtitlesInputComponent } from './components/subtitles-input/subtitles-input.component';
import { ModalComponent } from '../abstract/modal/modal.component';
import { ModalService } from '../abstract/modal/modal.service';
import { TranslationService } from './components/translation-modal/translation.service';
import { TranslationModalComponent } from './components/translation-modal/translation-modal.component';
import { TranslationModalService } from './components/translation-modal/translation-modal.service';

@NgModule({
    imports: [
        CommonModule,
        MaterialRoutingModule,
        FormsModule
    ],
    declarations: [
        MaterialComponent,
        ImageInputComponent,
        SubtitlesInputComponent,
        ModalComponent,
        TranslationModalComponent
    ],
    providers: [
        MaterialService,
        ModalService,
        TranslationService,
        TranslationModalService
    ]
})

export class MaterialModule { }