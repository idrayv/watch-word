import { Inject, Injectable, Optional } from '@angular/core';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { API_BASE_URL } from 'shared/service-proxies/service-proxies';

@Injectable()
export class MaterialService {
  private baseUrl: string;
  private options = {
    headers: new HttpHeaders({
      'Authorization': 'Bearer ' + abp.auth.getToken()
    })
  };

  constructor(private http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    this.baseUrl = baseUrl ? baseUrl : '';
  }

  public async parseSubtitles(subtitlesFile: any): Promise<any> {
    const input = new FormData();
    input.append('file', subtitlesFile);

    try {
      return this.http.post(this.baseUrl + '/api/parse/file', input, this.options).toPromise();
    } catch (response) {
      return this.getConnectionError(response);
    }
  }

  public async parseImage(imageFile: any): Promise<any> {
    const input = new FormData();
    input.append('file', imageFile);

    try {
      return this.http.post(this.baseUrl + '/api/image/parse', input, this.options).toPromise();
    } catch (response) {
      return this.getConnectionError(response);
    }
  }

  protected getConnectionError(response: any): any {
    let error = 'Server error. Please try again later.';
    if (response && response.error && response.error.error && response.error.error.message) {
      error = response.error.error.message;
    }

    return {
      error: [error],
      success: false
    };
  }
}
