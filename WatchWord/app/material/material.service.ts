import { Injectable } from '@angular/core';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import { MaterialPostResponseModel, ParseResponseModel } from '../material/material.models';
import { ImageResponseModel, Material as MaterialModel } from '../material/material.models';
import { MaterialResponseModel, Word, VocabWord, VocabType } from '../material/material.models';
import { BaseResponseModel } from '../global/models';
import { BaseService } from "../global/base-service";

@Injectable()
export class MaterialService extends BaseService {
    constructor() {
        super();
    }

    public parseSubtitles(subtitlesFile: any): Promise<ParseResponseModel> {
        let input = new FormData();
        input.append('file', subtitlesFile);

        return this.http.post<ParseResponseModel>(this.baseUrl + '/parse/file', input).toPromise()
            .catch(() => { return this.getConnectionError<ParseResponseModel>() });
    }

    public parseImage(imageFile: any): Promise<ImageResponseModel> {
        let input = new FormData();
        input.append('file', imageFile);

        return this.http.post<ImageResponseModel>(this.baseUrl + '/image/parse', input).toPromise()
            .catch(() => { return this.getConnectionError<ImageResponseModel>() });
    }

    public getMaterial(id: number): Promise<MaterialResponseModel> {
        return this.http.get<MaterialResponseModel>(this.baseUrl + '/material/' + id).toPromise()
            .catch(() => { return this.getConnectionError<MaterialResponseModel>() });
    }

    public saveMaterial(material: MaterialModel,
        vocabWords: VocabWord[]): Promise<MaterialPostResponseModel> {

        material.words = vocabWords.map((vocabWord) => {
            return <Word>{
                id: 0,
                theWord: vocabWord.word,
                count: material.words.find(w => w.theWord === vocabWord.word).count
            };
        });

        return this.http.post<MaterialPostResponseModel>(this.baseUrl + '/material/save', material).toPromise()
            .catch(() => { return this.getConnectionError<MaterialPostResponseModel>() });
    }

    public deleteMaterial(id: number): Promise<BaseResponseModel> {
        return this.http.delete<BaseResponseModel>(this.baseUrl + '/material/' + id).toPromise()
            .catch(() => { return this.getConnectionError<BaseResponseModel>() });
    }
}