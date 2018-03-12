import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MaterialComponent} from './material.component';
import {MaterialRoutingModule} from './material-routing.module';
import {FormsModule} from '@angular/forms';
import {MaterialService} from './material.service';
import {ImageInputComponent} from './components/image-input/image-input.component';
import {SubtitlesInputComponent} from './components/subtitles-input/subtitles-input.component';
import {TranslationModalModule} from '../global/components/translation-modal/translation-modal.module';
import {WordModule} from '../global/components/word/word.module';
import {VocabWordFiltrationPipe} from './pipes/vocab-word-filtration.pipe';
import {FavoriteMaterialsService} from '../global/favorite-materials/favorite-materils.service';
import {HttpClientModule} from '@angular/common/http';

@NgModule({
    imports: [CommonModule, MaterialRoutingModule, FormsModule, TranslationModalModule, WordModule, HttpClientModule],
    declarations: [MaterialComponent, ImageInputComponent, SubtitlesInputComponent, VocabWordFiltrationPipe],
    providers: [MaterialService, FavoriteMaterialsService]
})

export class MaterialModule {
}
