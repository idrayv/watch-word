import { Injectable } from '@angular/core';
import { SessionServiceProxy, UserLoginInfoDto, TenantLoginInfoDto } from '@shared/service-proxies/service-proxies'
import { VocabWordType } from '@shared/service-proxies/service-proxies'
import { ApplicationInfoDto, GetCurrentLoginInformationsOutput } from '@shared/service-proxies/service-proxies'
import { AbpMultiTenancyService } from '@abp/multi-tenancy/abp-multi-tenancy.service'
import { AppEnums } from '@shared/AppEnums';

@Injectable()
export class AppSessionService {
  private _user: UserLoginInfoDto;
  private _tenant: TenantLoginInfoDto;
  private _application: ApplicationInfoDto;
  private _lastPickedVocabType: VocabWordType;

  constructor(private _sessionService: SessionServiceProxy,
              private _abpMultiTenancyService: AbpMultiTenancyService) {
  }

  get application(): ApplicationInfoDto {
    return this._application;
  }

  get user(): UserLoginInfoDto {
    return this._user;
  }

  get userId(): number {
    return this.user ? this.user.id : null;
  }

  get tenant(): TenantLoginInfoDto {
    return this._tenant;
  }

  get lastPickedVocabType(): VocabWordType {
    return this._lastPickedVocabType;
  }

  set lastPickedVocabType(vocabType: VocabWordType) {
    this._lastPickedVocabType = vocabType;
  }

  getShownLoginName(): string {
    if (!this._user) {
      return '';
    }

    const userName = this._user.userName;
    if (!this._abpMultiTenancyService.isEnabled) {
      return userName;
    }

    return (this._tenant ? this._tenant.tenancyName : '.') + '\\' + userName;
  }

  init(): Promise<boolean> {
    return new Promise<boolean>((resolve, reject) => {
      this._sessionService.getCurrentLoginInformations().toPromise().then((result: GetCurrentLoginInformationsOutput) => {
        this._application = result.application;
        this._user = result.user;
        this._tenant = result.tenant;
        this._lastPickedVocabType = AppEnums.VocabType.LearnWord;

        resolve(true);
      }, (err) => {
        reject(err);
      });
    });
  }
}
