import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import { BaseResponseModel } from '../global/models';
import { BaseService } from '../global/base-service';
import { LoginModel, RegisterModel } from './auth.models';
import { AccountService } from './account.service';
import { AuthResponseModel } from './auth.models';

@Injectable()
export class AuthService extends BaseService {
    constructor() {
        super();
    }

    authenticate(loginModel: LoginModel): Promise<AuthResponseModel> {
        return this.http.post<AuthResponseModel>(this.baseUrl + '/account/login', loginModel).toPromise()
            .catch(() => this.getConnectionError<AuthResponseModel>());
    }

    register(registerModel: RegisterModel): Promise<AuthResponseModel> {
        return this.http.post<AuthResponseModel>(this.baseUrl + '/account/register', registerModel).toPromise()
            .catch(() => this.getConnectionError<AuthResponseModel>());
    }

    logout(): Promise<BaseResponseModel> {
        return this.http.post<BaseResponseModel>(this.baseUrl + '/account/logout', {}).toPromise()
            .catch(() => this.getConnectionError<BaseResponseModel>());
    }
}
