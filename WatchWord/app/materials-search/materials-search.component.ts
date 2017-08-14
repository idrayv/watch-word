import { Component, ElementRef, OnDestroy, OnInit, ViewContainerRef, ApplicationRef } from '@angular/core';
import { MaterialsSearchModel, RequestStatus } from './materials-search.models';
import { MaterialsSearchService } from './materials-search.service';
import { Subscription } from 'rxjs/Subscription';
import { ServiceLocator } from "../global/service-locator";
import { ToastService } from "../global/toast/toast.service";
import { BaseComponent } from "../global/base.component";

@Component({
    selector: 'materials-search',
    templateUrl: 'app/materials-search/materials-search.template.html',
    host: {
        '(document:click)': 'documentClick($event.target)'
    }
})

export class MaterialsSearchComponent extends BaseComponent implements OnDestroy, OnInit {
    model: MaterialsSearchModel = new MaterialsSearchModel();
    searchSubscription: Subscription = new Subscription();
    isFocused: boolean = false;
    status: RequestStatus = RequestStatus.NotStarted;
    timer: any;

    constructor(private searchSevice: MaterialsSearchService, private componentElement: ElementRef) {
        super();
    }

    get isLoadingVisible(): boolean {
        return this.status === RequestStatus.InProgress;
    }

    inputChanged(): void {
        if (!this.isFocused) {
            this.isFocused = true;
        }

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
                    this.displayConnectionError();
                }
            );
        }, 300);
    }

    processErrors(errors: string[]) {
        this.status = RequestStatus.CompletedWithError;

        errors.forEach((err) => {
            this.displayError(err);
        })
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