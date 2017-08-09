import { Component, ElementRef, OnDestroy, OnInit } from '@angular/core';
import { MaterialsSearchModel, RequestStatus } from './materials-search.models';
import { MaterialsSearchService } from './materials-search.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'materials-search',
    templateUrl: 'app/materials-search/materials-search.template.html',
    host: {
        '(document:click)': 'documentClick($event.target)'
    }
})

export class MaterialsSearchComponent implements OnDestroy, OnInit {
    model: MaterialsSearchModel = new MaterialsSearchModel();
    searchSubscription: Subscription = new Subscription();
    isFocused: boolean = false;
    status: RequestStatus = RequestStatus.NotStarted;
    timer: any;

    get isLoadingVisible(): boolean {
        return this.status === RequestStatus.InProgress;
    }

    constructor(private searchSevice: MaterialsSearchService, private componentElement: ElementRef) { }

    inputChanged(): void {
        this.searchSubscription.unsubscribe();
        this.status = RequestStatus.InProgress;
        clearTimeout(this.timer);

        this.timer = setTimeout(() => {
            let text: string = this.model.input || '';

            this.searchSubscription = this.searchSevice.search(text).subscribe(
                response => {
                    this.model.entities = response.entities;

                    if (!response.success) {
                        this.processErrors(response.errors);
                    } else {
                        this.status = RequestStatus.NotStarted;
                    }
                },
                err => {
                    this.processErrors(['Connection error occured.'])
                }
            );
        }, 300);
    }

    processErrors(errors: string[]) {
        this.status = RequestStatus.CompletedWithError;
        errors.forEach(err => console.log(err))
    }

    clearInput(): void {
        this.model.input = '';
        this.model.entities = [];
    }

    private documentClick(target: ElementRef): void {
        if (!this.componentElement.nativeElement.contains(target)) {
            this.isFocused = false;
        } else {
            if (this.isFocused === false) {
                if (this.status !== RequestStatus.NotStarted) {
                    this.model.entities = [];
                    this.inputChanged();
                }
                this.isFocused = true;
            }
        }
    }

    ngOnInit(): void {
        this.searchSubscription.closed = true;
    }

    ngOnDestroy(): void {
        this.searchSubscription.unsubscribe();
    }
}