import {Component, OnInit, OnDestroy, Injector} from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import {ISubscription} from 'rxjs/Subscription';
import {MaterialsModel} from './materials.models';
import {CountResponseModel, PaginationResponseModel} from '../global/components/pagination/pagination.models';
import {MaterialsPaginationService} from './materials-pagination.service';
import {Material as MaterialModel} from '../material/material.models';
import {AppComponentBase} from '@shared/app-component-base';

@Component({
    templateUrl: 'materials.template.html'
})

export class MaterialsComponent extends AppComponentBase implements OnInit, OnDestroy {
    public model: MaterialsModel = new MaterialsModel();
    private routeSubscription: ISubscription;
    private itemsPerPage = 24;
    private materialsRoute = '/materials/page';

    constructor(private router: Router,
                private route: ActivatedRoute,
                private materialsService: MaterialsPaginationService,
                injector: Injector) {
        super(injector);
    }

    ngOnInit(): void {
        this.routeSubscription = this.route.params.subscribe(param => this.onRouteChanged(param['id']));
    }

    public onMaterialClick(id: number): void {
        this.router.navigate(['/material', id]);
    }

    private onRouteChanged(param: string): void {
        if (!param) {
            this.changeModel(1);
        } else if (+param) {
            this.changeModel(+param);
        } else {
            this.router.navigate(['/404']);
        }
    }

    private changeModel(page: number) {
        this.model.materials = [];
        abp.ui.setBusy('body');
        this.materialsService.getCount().then(response => this.fillPaginationModel(response, page));
        this.materialsService.getEntities(page, this.itemsPerPage).then((response) => {
            abp.ui.clearBusy('body');
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
            if (response && response.errors) {
                response.errors.forEach(err => this.displayError(err));
            }
        }
    }

    private fillMaterials(response: PaginationResponseModel<MaterialModel>): void {
        if (response.success) {
            this.model.materials = response.entities;
        } else {
            if (response && response.errors) {
                response.errors.forEach(err => this.displayError(err));
            }
        }
    }

    ngOnDestroy(): void {
        this.routeSubscription.unsubscribe();
    }
}
