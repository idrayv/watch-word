import { Component, OnInit } from '@angular/core';
import { SettingsService } from './settings.service';
import { SettingsResponseModel, SettingsModel } from './settings.models';
import { BaseComponent } from '../global/base-component';

@Component({
    templateUrl: 'app/settings/settings.template.html'
})

export class SettingsComponent extends BaseComponent implements OnInit {
    public model: SettingsModel = new SettingsModel();

    constructor(private settingService: SettingsService) {
        super();
    }

    ngOnInit(): void {
        this.settingService.getUnfilledSiteSettings().then(response => this.initializeModel(response));
    }

    private initializeModel(response: SettingsResponseModel): void {
        if (response.success) {
            this.model.settings = response.settings;
        } else {
            this.displayErrors(response.errors, 'Settings error');
        }
    }

    public submitForm(): void {
        this.settingService.insertSettins(this.model.settings).then(response => {
            // TODO: Saved message or handle errors
        });
    }
}