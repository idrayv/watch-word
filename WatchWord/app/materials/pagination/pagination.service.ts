import { Injectable } from '@angular/core';
import { PaginationServiceModel, PaginationModel } from './pagination.models'

@Injectable()
export class PaginationService {
    getPaginator(model: PaginationModel): PaginationServiceModel {
        let totalPages = Math.ceil(model.count / model.itemsPerPage);
        let startPage: number, endPage: number;

        if (totalPages < 10) {
            startPage = 1;
            endPage = totalPages;
        } else {
            if (model.currentPage <= 6) {
                startPage = 1;
                endPage = 10;
            } else if (model.currentPage + 4 >= totalPages) {
                startPage = totalPages - 9;
                endPage = totalPages;       
            } else {
                startPage = model.currentPage - 5;
                endPage = model.currentPage + 4;
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

    private range(start: number, end: number): Array<number> {
        let result: Array<number> = []
        while (end >= start) {
            result.push(start++);
        }
        return result;
    }
}