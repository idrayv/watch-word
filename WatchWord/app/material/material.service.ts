import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import { CreateResponseModel, ParseResponseModel, ImageResponseModel, MaterialModel, MaterialResponseModel } from '../material/material.models';
let cfg = require('../config').appConfig;

@Injectable()
export class MaterialService {
    private baseUrl: string = cfg.apiRoute;
    private connectionErrorModel = { sucess: false, errors: ['Connection error'] };

    constructor(private http: Http) { }

    parseSubtitles(subtitlesFile: any): Promise<ParseResponseModel> {
        let input = new FormData();
        input.append('file', subtitlesFile);

        return this.http.post(this.baseUrl + '/parse/file', input).toPromise()
            .then((res: Response) => res.json())
            .catch(() => { return this.connectionErrorModel });
    }

    parseImage(imageFile: any): Promise<ImageResponseModel> {
        let input = new FormData();
        input.append('file', imageFile);

        return this.http.post(this.baseUrl + '/image/parse', input).toPromise()
            .then((res: Response) => res.json())
            .catch(() => { return this.connectionErrorModel });
    }

    getMaterial(id: number): Promise<MaterialResponseModel> {
        return this.http.get(this.baseUrl + '/material/' + id).toPromise()
            .then((res: Response) => res.json())
            .catch(() => { return this.connectionErrorModel });
    }

    saveMaterial(material: MaterialModel): Promise<CreateResponseModel> {
        return this.http.post(this.baseUrl + '/material/save', material).toPromise()
            .then((res: Response) => res.json())
            .catch(() => { return this.connectionErrorModel });
    }

    deleteMaterial(id: number): Promise<CreateResponseModel> {
        return this.http.delete(this.baseUrl + '/material/' + id).toPromise()
            .then((res: Response) => res.json())
            .catch(() => { return this.connectionErrorModel });
    }
}