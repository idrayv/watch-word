import { Injectable } from '@angular/core';
import { PaginationService } from '../global/components/pagination/pagination.service';
import { Material as MaterialModel } from '../material/material.models';

@Injectable()
export class MaterialsPaginationService extends PaginationService<MaterialModel> {
    constructor() {
        super('Materials');
    }
}