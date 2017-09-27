import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SettingsComponent } from './settings.component';
import { SettingsRoutingModule } from './settings-routing.module';
import { SettingsService } from './settings.service';

@NgModule({
    imports: [CommonModule, SettingsRoutingModule, FormsModule],
    declarations: [SettingsComponent],
    providers: [SettingsService]
})

export class SettingsModule {
}
