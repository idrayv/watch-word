import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs';
import { TranslatePostResponseModel, VocabularyPostResponseModel, TranslationModalModel } from './translation-modal.models';
import { TranslationService } from './translation.service';
import { VocabWord } from "../../../material/material.models";
let cfg = require('../../../config').appConfig;

@Injectable()
export class TranslationModalService {
    private baseUrl: string = cfg.apiRoute;
    private translationModel: Subject<TranslationModalModel> = new Subject<TranslationModalModel>();

    public constructor(private http: Http, private translationService: TranslationService) { }

    public get translationModalObservable(): Observable<TranslationModalModel> {
        return this.translationModel.asObservable();
    }

    public pushToModal(vocabWord: VocabWord): void {
        this.translationService.getTransletion(vocabWord.word).then(response => {
            this.translationModel.next(this.getTransletionModel(vocabWord, response));
        });
    }

    private getTransletionModel(vocabWord: VocabWord, serverResponse: TranslatePostResponseModel): TranslationModalModel {
        let model: TranslationModalModel = new TranslationModalModel();
        model.vocabWord = vocabWord;
        if (serverResponse.success) {
            model.translations = serverResponse.translations;
        } else {
            // TODO: handle errors here
        }
        return model;
    }

    public saveToVocabulary(vocabWord: VocabWord): void {
        this.translationService.saveToVocabulary(vocabWord).then(response => {
            //handle response here
        });
    }
}