import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ISubscription } from 'rxjs/Subscription';
import { MaterialsModel } from './materials.models';
import { SpinnerService } from '../global/spinner/spinner.service';
import { CountResponseModel, PaginationResponseModel } from '../global/components/pagination/pagination.models';
import { MaterialsPaginationService } from "./materials-pagination.service";
import { MaterialModel } from "../material/material.models";

@Component({
    templateUrl: 'app/materials/materials.template.html'
})

export class MaterialsComponent implements OnInit, OnDestroy {
    public model: MaterialsModel = new MaterialsModel();
    private routeSubscription: ISubscription;
    private itemsPerPage: number = 24;
    private materialsRoute: string = '/materials/page';

    constructor(private router: Router, private route: ActivatedRoute,
        private materialsService: MaterialsPaginationService,
        private spinner: SpinnerService) { }

    ngOnInit(): void {
        this.routeSubscription = this.route.params.subscribe(param => this.onRouteChanged(+param['id']));
    }

    public onMaterialClick(id: number): void {
        this.router.navigate(['/material', id]);
    }

    private onRouteChanged(id: number): void {
        let currentPage: number = !id ? 1 : id;
        this.changeModel(currentPage);
    }

    private changeModel(page: number) {
        this.model.materials = [];
        this.spinner.displaySpinner(true);
        this.materialsService.getCount().then(response => this.fillPaginationModel(response, page));
        this.materialsService.getEntities(page, this.itemsPerPage).then((response) => {
            this.spinner.displaySpinner(false);
            return this.fillMaterials(response);
        });
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

    private fillMaterials(response: PaginationResponseModel<MaterialModel>): void {
        if (response.success) {
            this.model.materials = response.entities;
        } else {
            this.model.serverErrors = response.errors;
        }
    }

    ngOnDestroy(): void {
        this.routeSubscription.unsubscribe();
    }
}