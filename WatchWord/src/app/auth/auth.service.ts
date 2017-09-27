import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import { BaseResponseModel } from '../global/models';
import { BaseService } from '../global/base-service';
import { LoginModel, RegisterModel } from './auth.models';
import { UserService } from './user.service';
import { UserModel } from './auth.models';

@Injectable()
export class AuthService extends BaseService {
    constructor() {
        super();
    }

    authenticate(loginModel: LoginModel): Promise<BaseResponseModel> {
        return this.http.post<BaseResponseModel>(this.baseUrl + '/account/login', loginModel).toPromise()
            .catch(() => this.getConnectionError<BaseResponseModel>());
    }

    register(registerModel: RegisterModel): Promise<BaseResponseModel> {
        return this.http.post<BaseResponseModel>(this.baseUrl + '/account/register', registerModel).toPromise()
            .catch(() => this.getConnectionError<BaseResponseModel>());
    }

    logout(): Promise<BaseResponseModel> {
        return this.http.post<BaseResponseModel>(this.baseUrl + '/account/logout', {}).toPromise()
            .catch(() => this.getConnectionError<BaseResponseModel>());
    }
}
