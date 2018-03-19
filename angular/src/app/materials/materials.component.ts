import {Component, OnInit, OnDestroy, Injector} from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import {ISubscription} from 'rxjs/Subscription';
import {MaterialsModel} from './materials.models';
import {AppComponentBase} from '@shared/app-component-base';
import {MaterialServiceProxy, Material} from '@shared/service-proxies/service-proxies';

@Component({
    templateUrl: 'materials.template.html'
})

export class MaterialsComponent extends AppComponentBase implements OnInit, OnDestroy {
    public model: MaterialsModel = new MaterialsModel();
    private routeSubscription: ISubscription;
    private itemsPerPage = 24;
    private materialsRoute = '/materials/page';

    constructor(private router: Router,
                private materialService: MaterialServiceProxy,
                private route: ActivatedRoute,
                injector: Injector) {
        super(injector);
    }

    ngOnInit(): void {
        this.routeSubscription = this.route.params.subscribe(param => this.onRouteChanged(param['id']));
    }

    public onMaterialClick(id: number): void {
        this.router.navigate(['app/material', id]);
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
        abp.ui.setBusy();
        this.materialService.getCount().subscribe(count => this.fillPaginationModel(count, page));

        this.materialService.getMaterials(page, this.itemsPerPage).subscribe((response) => {
            abp.ui.clearBusy();
            return this.fillMaterials(response);
        });
    }

    private fillPaginationModel(count: number, page: number): void {
        this.model.paginationModel = {
            count: count,
            currentPage: page,
            itemsPerPage: this.itemsPerPage,
            route: this.materialsRoute
        };
    }

    private fillMaterials(materials: Material[]): void {
        this.model.materials = materials;
    }

    ngOnDestroy(): void {
        this.routeSubscription.unsubscribe();
    }
}
