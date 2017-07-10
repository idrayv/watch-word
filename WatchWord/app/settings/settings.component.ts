import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ISubscription } from 'rxjs/Subscription';
import { SettingsService } from './settings.service';
import { SettingsResponseModel, SettingsModel, SettingKey } from './settings.models';

@Component({
    templateUrl: 'app/settings/settings.template.html'
})

export class SettingsComponent implements OnInit {
    public model: SettingsModel = new SettingsModel();

    constructor(private settingService: SettingsService) { }

    ngOnInit(): void {
        this.settingService.getUnfilledSiteSettings().then(response => this.initializeModel(response));
    }

    private initializeModel(response: SettingsResponseModel): void {
        if (response.success) {
            this.model.settings = response.settings;
        } else {
            this.model.serverErrors = ['Server error occured.'];
        }
    }

    public submitForm(): void {
        this.settingService.insertSettins(this.model.settings).then(response => {
            // TODO: Saved message or handle errors
        });
    }
}