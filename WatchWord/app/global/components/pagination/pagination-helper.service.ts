import { Injectable } from '@angular/core';
import { PaginationServiceModel, PaginationModel } from './pagination.models'

@Injectable()
export class PaginationHelperService {
    getPaginator(model: PaginationModel): PaginationServiceModel {
        let totalPages = Math.ceil(model.count / model.itemsPerPage);
        let startPage: number, endPage: number;

        if (totalPages < 6) {
            startPage = 1;
            endPage = totalPages;
        } else {
            if (model.currentPage <= 4) {
                startPage = 1;
                endPage = 6;
            } else if (model.currentPage + 2 >= totalPages) {
                startPage = totalPages - 5;
                endPage = totalPages;
            } else {
                startPage = model.currentPage - 2;
                endPage = model.currentPage + 3;
            }
        }

        let pages: number[] = this.range(startPage, endPage);

        let result: PaginationServiceModel =
            {
                currentPage: model.currentPage,
                totalPages: totalPages,
                startPage: startPage,
                endPage: endPage,
                pages: pages,
                route: model.route
            };

        return result;
    }

    private range(start: number, end: number): number[] {
        let result: number[] = []
        while (end >= start) {
            result.push(start++);
        }
        return result;
    }
}