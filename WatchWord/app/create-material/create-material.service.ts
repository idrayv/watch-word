import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Http, Response } from "@angular/http";
import { ParseResponseModel } from "./material.models";
import "rxjs/add/operator/map";
let cfg = require('../config').appConfig;

@Injectable()
export class CreateMaterialService {
    private baseUrl: string;

    constructor(private http: Http) {
        this.baseUrl = cfg.apiRoute;
    }

    parseSubtitles(subtitlesFile: any): Observable<ParseResponseModel> {
        let input = new FormData();
        input.append("file", subtitlesFile);

        return this.http.post(this.baseUrl + "/parse/file", input).map((res: Response) => res.json());;
    }
}