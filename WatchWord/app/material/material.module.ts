import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialComponent } from './material.component'
import { MaterialRoutingModule } from './material-routing.module'
import { FormsModule } from '@angular/forms';
import { MaterialService } from './material.service';
import { ImageInputComponent } from './components/image-input/image-input.component';
import { SubtitlesInputComponent } from './components/subtitles-input/subtitles-input.component';
import { TranslationModalComponent } from "../global/components/translation-modal/translation-modal.component";
import { ModalComponent } from "../global/components/modal/modal.component";
import { TranslationService } from "../global/components/translation-modal/translation.service";
import { TranslationModalService } from "../global/components/translation-modal/translation-modal.service";
import { ModalService } from "../global/components/modal/modal.service";

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