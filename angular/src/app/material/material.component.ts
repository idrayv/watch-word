import {NgForm, NgModel} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {Component, OnInit, OnDestroy, Injector} from '@angular/core';
import {ISubscription} from 'rxjs/Subscription';
import {MaterialMode, MaterialStats} from './material.models';
import {VocabWordFiltration} from './material.models';
import {TranslationModalService} from '../global/components/translation-modal/translation-modal.service';
import {ComponentValidation} from '../global/component-validation';
import {AppComponentBase} from '@shared/app-component-base';
import {Word, Material, VocabWord, VocabWordType} from 'shared/service-proxies/service-proxies';
import {MaterialServiceProxy, FavoriteMaterialServiceProxy} from 'shared/service-proxies/service-proxies';

@Component({
    templateUrl: 'material.template.html'
})
export class MaterialComponent extends AppComponentBase implements OnInit, OnDestroy {
    public mode: MaterialMode = null;
    public vocabWords: VocabWord[] = [];
    public material: Material = new Material();
    public isFavorite = false;
    public formSubmitted = false;
    public filtration: VocabWordFiltration = new VocabWordFiltration();
    private routeSubscription: ISubscription;
    private translationModalResponseSubscription: ISubscription;

    constructor(private materialService: MaterialServiceProxy,
                private route: ActivatedRoute,
                private router: Router,
                private favoriteMaterialsService: FavoriteMaterialServiceProxy,
                private translationModalService: TranslationModalService,
                injector: Injector) {
        super(injector);
    }

    ngOnInit() {
        this.routeSubscription = this.route.params.subscribe(params => this.onRouteChanged(params['id']));
        this.translationModalResponseSubscription = this.translationModalService.translationModalResponseObservable
            .subscribe(vocabWord => {
                this.translationModalService.updateVocabWordInCollection(vocabWord, this.vocabWords);
            });
    }

    public saveMaterial(form: NgForm): void {
        this.formSubmitted = true;
        if (form.valid) {
            abp.ui.setBusy('body');

            this.material.words = this.vocabWords.map((vocabWord) => {
                const word = new Word();
                word.id = 0;
                word.theWord = vocabWord.word;
                word.count = this.material.words.find(w => w.theWord === vocabWord.word).count;

                return word;
            });

            this.materialService.save(this.material)
                .finally(() => {
                    abp.ui.clearBusy('body');
                    this.formSubmitted = false;
                })
                .subscribe((response) => {
                    if (this.mode === MaterialMode.Add) {
                        this.router.navigateByUrl('app/material/' + response.id);
                    } else {
                        this.mode = MaterialMode.Read;
                    }
                });
        }
    }

    public editMaterial(): void {
        this.mode = MaterialMode.Edit;
    }

    public deleteMaterial(): void {
        abp.ui.setBusy('body');
        this.materialService.delete(this.material.id).subscribe(() => {
            abp.ui.clearBusy('body');
            this.router.navigateByUrl('materials');
        });
    }

    get materialStats(): MaterialStats[] {
        const stats: MaterialStats[] = [];

        const totalCount = this.material.words
            .map(w => w.count).reduce((pre, curr) => pre + curr, 0);

        const uniqueCount = this.vocabWords.length;
        this.pushStatToStats(stats, 'Total words', totalCount.toString());
        this.pushStatToStats(stats, 'Unique words', uniqueCount.toString());

        if (this.appSession.user && this.appSession.user.id) {
            const learnCount = this.vocabWords.filter(v => v.type === VocabWordType._0).length;
            const knownCount = this.vocabWords.filter(v => v.type === VocabWordType._1).length;

            this.pushStatToStats(stats, 'Learn words', learnCount.toString());
            this.pushStatToStats(stats, 'Known words', knownCount.toString());
            this.pushStatToStats(stats, 'Unsigned words', (uniqueCount - (learnCount + knownCount)).toString());
        }

        return stats;
    }

    get isEditButtonsVisible(): boolean {
        if (!this.appSession.userId || !this.material.owner) {
            return false;
        }

        const isAdmin = this.isGranted('Admin');
        return this.material.owner.id === this.appSession.userId || isAdmin;
    }

    get isAddToFavoritesButtonVisible(): boolean {
        if (!this.appSession.userId || this.mode === MaterialMode.Add) {
            return false;
        }
        return true;
    }

    public addToFavorites(): void {
        abp.ui.setBusy('body');
        this.favoriteMaterialsService.post(this.material.id)
            .finally(() => abp.ui.clearBusy('body'))
            .subscribe(() => this.isFavorite = !this.isFavorite);
    }

    public removeFromFavorites(): void {
        abp.ui.setBusy('body');
        this.favoriteMaterialsService.delete(this.material.id)
            .finally(() => abp.ui.clearBusy('body'))
            .subscribe(resp => this.isFavorite = !this.isFavorite);
    }

    public validationErrors(state: NgModel): string[] {
        return ComponentValidation.validationErrors(state);
    }

    private onRouteChanged(param: string): void {
        if (param === 'create') {
            this.mode = MaterialMode.Add;
            this.material = new Material();
            this.vocabWords = [];
        } else if (+param) {
            this.initializeMaterial(+param);
        } else {
            this.router.navigate(['/404']);
        }
    }

    private initializeMaterial(id: number): void {
        abp.ui.setBusy('body');

        this.materialService.getMaterial(id).subscribe(response => {
            this.mode = MaterialMode.Read;
            this.material = response.material;
            this.vocabWords = response.vocabWords;

            if (this.appSession.getShownLoginName()) {
                this.favoriteMaterialsService.get(this.material.id)
                    ._finally(() => abp.ui.clearBusy('body'))
                    .subscribe(isFavorite => this.isFavorite = isFavorite);
            } else {
                abp.ui.clearBusy('body');
            }
        });
    }

    private pushStatToStats(stats: MaterialStats[], name: string, value: string) {
        stats.push({
            name: name,
            value: value
        });
    }

    ngOnDestroy() {
        this.routeSubscription.unsubscribe();
        this.translationModalResponseSubscription.unsubscribe();
    }
}
