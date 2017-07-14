import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { DictionariesModel, DictionariesResponseModel } from './dictionaris.models';
import { DictionariesService } from './dictionaries.service';
import { VocabWord, VocabType, WordComposition } from '../material/material.models';
import { TranslationModalService } from '../global/components/translation-modal/translation-modal.service';

@Component({
    templateUrl: 'app/dictionaries/dictionaries.template.html',
})

export class DictionariesComponent implements OnInit, OnDestroy {
    private model: DictionariesModel = new DictionariesModel();
    private modalResponse: ISubscription;

    constructor(private dictionariesService: DictionariesService, private transletionModalService: TranslationModalService) { }

    ngOnInit(): void {
        this.dictionariesService.getDictionaries().then(response => this.fillModelFromResponse(response));
        // TODO: mix with the same method in material component
        this.modalResponse = this.transletionModalService.transletionModalResponseObserverable.subscribe(response => {
            if (response.success) {
                let index = this.model.wordCompositions.findIndex(c => c.materialWord.theWord === response.wordComposition.materialWord.theWord);
                this.model.wordCompositions[index] = response.wordComposition;
            } else {
                this.model.serverErrors = response.errors;
            }
        });
    }

    private fillModelFromResponse(response: DictionariesResponseModel): void {
        if (response.success) {
            this.model.wordCompositions = response.vocabWords.map(v => {
                return { vocabWord: v, materialWord: { theWord: v.word, count: 0, id: 0 } };
            });
        } else {
            this.model.serverErrors = response.errors;
        }
    }

    public learnWords(): WordComposition[] {
        return this.model.wordCompositions.filter(word => word.vocabWord.type === VocabType.LearnWord);
    }

    public knownWords(): WordComposition[] {
        return this.model.wordCompositions.filter(word => word.vocabWord.type === VocabType.KnownWord);
    }

    ngOnDestroy(): void {
        this.modalResponse.unsubscribe();
    }
}