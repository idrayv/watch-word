import {Injectable} from '@angular/core';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import {BaseResponseModel} from '../global/models';
import {LoginModel, RegisterModel} from './auth.models';
import {AuthResponseModel} from './auth.models';

@Injectable()
export class AuthService {
    constructor() {
    }

    authenticate(loginModel: LoginModel): Promise<AuthResponseModel> {
        /*return this.http.post<AuthResponseModel>(this.baseUrl + '/account/login', loginModel).toPromise()
            .catch(() => this.getConnectionError<AuthResponseModel>());*/
        return new Promise(function(resolve) {
            resolve(new AuthResponseModel());
        })
    }

    register(registerModel: RegisterModel): Promise<AuthResponseModel> {
        /*return this.http.post<AuthResponseModel>(this.baseUrl + '/account/register', registerModel).toPromise()
            .catch(() => this.getConnectionError<AuthResponseModel>());*/
        return new Promise(function(resolve) {
            resolve(new AuthResponseModel());
        })
    }

    logout(): Promise<BaseResponseModel> {
        /*return this.http.post<BaseResponseModel>(this.baseUrl + '/account/logout', {}).toPromise()
            .catch(() => this.getConnectionError<BaseResponseModel>());*/
        return new Promise(function(resolve) {
            resolve(new BaseResponseModel());
        })
    }
}
