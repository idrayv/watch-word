import { BaseResponseModel } from '../global/models';

export enum SettingType {
    Int,
    String,
    Boolean,
    Date
}

export enum SettingKey {
    yandexTranslateApiKey,
    yandexDictionaryApiKey
}

export class Setting {
    public key: SettingKey;
    public type: SettingType;
    public int: number;
    public string: string;
    public boolean: boolean;
    public date?: Date
}

export class SettingsResponseModel extends BaseResponseModel {
    public settings: Setting[];
}

export class SettingsModel {

    private settingNames: { key: SettingKey, value: string }[];
    public serverErrors: string[];
    public settings: Setting[] = [];

    get hasErrors(): boolean {
        return !!(this.serverErrors && this.serverErrors.length > 0);
    }

    constructor() {
        this.settingNames = [
            { key: SettingKey.yandexTranslateApiKey, value: 'Yandex Translate Api Key' },
            { key: SettingKey.yandexDictionaryApiKey, value: 'Yandex Dictionary Api Key' }
        ];
    }

    public getNameByKey(key: SettingKey): string {
        let settingName = this.settingNames.find(n => n.key === key);
        return settingName ? settingName.value : '';
    }
}