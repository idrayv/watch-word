import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialComponent } from './material.component';
import { MaterialRoutingModule } from './material-routing.module';
import { FormsModule } from '@angular/forms';
import { MaterialService } from './material.service';
import { ImageInputComponent } from './components/image-input/image-input.component';
import { SubtitlesInputComponent } from './components/subtitles-input/subtitles-input.component';
import { TranslationModalModule } from '../global/components/translation-modal/translation-modal.module';
import { ModalModule } from '../global/components/modal/modal.module';
import { WordModule } from '../global/components/word/word.module';

@NgModule({
    imports: [CommonModule, MaterialRoutingModule, FormsModule, ModalModule, TranslationModalModule, WordModule],
    declarations: [MaterialComponent, ImageInputComponent, SubtitlesInputComponent],
    providers: [MaterialService]
})

export class MaterialModule {
}