import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ISubscription } from 'rxjs/Subscription';
import { MaterialsService } from './materials.service';
import { PaginationModel } from './pagination/pagination.models';
import { MaterialsModel } from './materials.models';
import { CountResponseModel, MaterialsResponseModel } from './materials.models';

@Component({
    templateUrl: 'app/materials/materials.template.html'
})

export class MaterialsComponent implements OnInit, OnDestroy {
    private routeSubscription: ISubscription;
    public model: MaterialsModel = new MaterialsModel();
    private itemsPerPage: number = 5;
    private materialsRoute: string = '/materials/page';

    constructor(private route: ActivatedRoute, private materialsService: MaterialsService) { }

    ngOnInit(): void {
        this.routeSubscription = this.route.params.subscribe(param => this.onRouteChanged(+param['id']));
    }

    private onRouteChanged(id: number): void {
        let currentPage: number = !id ? 1 : id;
        this.changeModel(currentPage);
    }

    private changeModel(page: number) {
        this.materialsService.getCount().then(response => this.fillPaginationModel(response, page));

        this.materialsService.getMaterials(page, this.itemsPerPage).then(response => this.fillMaterials(response));
    }

    private fillPaginationModel(response: CountResponseModel, page: number): void {
        if (response.success) {
            this.model.paginationModel = {
                count: response.count,
                currentPage: page,
                itemsPerPage: this.itemsPerPage,
                route: this.materialsRoute
            };
        } else {
            this.model.serverErrors = response.errors;
        }
    }

    private fillMaterials(response: MaterialsResponseModel): void {
        if (response.success) {
            this.model.materials = response.materials;
        } else {
            this.model.serverErrors = response.errors;
        }
    }

    ngOnDestroy(): void {
        this.routeSubscription.unsubscribe();
    }
}