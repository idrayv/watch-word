import { Component, OnInit, OnDestroy } from '@angular/core';
import { WordComposition } from "../../../material/material.models";

@Component({
    selector: 'word',
    templateUrl: 'app/global/components/word/word.template.html'
})

export class WordComponent implements OnInit, OnDestroy {
    public model: WordComposition;

    constructor() { }

    ngOnInit(): void {
    }

    ngOnDestroy(): void {
    }
}