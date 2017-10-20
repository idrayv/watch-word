import { Component, ElementRef, OnDestroy, OnInit, HostListener } from '@angular/core';
import { MaterialsSearchModel, RequestStatus } from './materials-search.models';
import { MaterialsSearchService } from './materials-search.service';
import { Subscription } from 'rxjs/Subscription';
import { BaseComponent } from '../global/base-component';

@Component({
    selector: 'ww-materials-search',
    templateUrl: 'materials-search.template.html'
})

export class MaterialsSearchComponent extends BaseComponent implements OnDestroy, OnInit {
    model: MaterialsSearchModel = new MaterialsSearchModel();
    searchSubscription: Subscription = new Subscription();
    isFocused = false;
    status: RequestStatus = RequestStatus.NotStarted;
    timer: any;

    constructor(private searchSevice: MaterialsSearchService, private componentElement: ElementRef) {
        super();
    }

    get isLoadingVisible(): boolean {
        return this.status === RequestStatus.InProgress;
    }

    @HostListener('document:click', ['$event.target'])
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

    public inputChanged(): void {
        if (!this.isFocused) {
            this.isFocused = true;
        }

        this.searchSubscription.unsubscribe();
        this.status = RequestStatus.InProgress;
        clearTimeout(this.timer);

        this.timer = setTimeout(() => {
            const text: string = this.model.input || '';

            this.searchSubscription = this.searchSevice.search(text).subscribe(response => {
                this.model.entities = response.entities;

                if (!response.success) {
                    this.processErrors(response.errors);
                } else {
                    this.status = RequestStatus.NotStarted;
                }
            }, () => {
                this.displayError('Server unavaliable.', 'Search error');
            });
        }, 300);
    }

    public processErrors(errors: string[]) {
        this.status = RequestStatus.CompletedWithError;
        this.displayErrors(errors, 'Search error');
    }

    public clearInput(): void {
        this.model.input = '';
        this.model.entities = [];
    }

    ngOnInit(): void {
        this.searchSubscription.closed = true;
    }

    ngOnDestroy(): void {
        this.searchSubscription.unsubscribe();
    }
}
