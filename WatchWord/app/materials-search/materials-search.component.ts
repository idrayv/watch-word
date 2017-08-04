import { Component } from '@angular/core';
import { MaterialsSearchModel } from './materials-search.models';
import { MaterialsSearchService } from './materials-search.service';

@Component({
    selector: 'materials-search',
    templateUrl: 'app/materials-search/materials-search.template.html'
})

export class MaterialsSearchComponent {
    constructor(private searchSevice: MaterialsSearchService) { }
    model: MaterialsSearchModel = new MaterialsSearchModel();

    inputChanged(): void {
        let text: string = this.model.input || "";
        this.searchSevice.search(text).then(response => {
            this.model.entities = response.entities;
            if (response.errors.length > 0)
                response.errors.forEach(error => console.log(error));
        });
    }

    clearInput(): void {
        this.model.input = "";
        this.model.entities = [];
    }
}