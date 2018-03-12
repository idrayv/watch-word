import {NgForm, NgModel} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {Component, OnInit, OnDestroy, Injector} from '@angular/core';
import {ISubscription} from 'rxjs/Subscription';
import {MaterialMode, MaterialStats} from './material.models';
import {VocabWordFiltration} from './material.models';
import {TranslationModalService} from '../global/components/translation-modal/translation-modal.service';
import {ComponentValidation} from '../global/component-validation';
import {FavoriteMaterialsService} from '../global/favorite-materials/favorite-materils.service';
import {AppComponentBase} from '@shared/app-component-base';
import {MaterialServiceProxy, Word, Material, VocabWord, VocabWordType} from 'shared/service-proxies/service-proxies';

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
                private favoriteMaterialsService: FavoriteMaterialsService,
                private translationModalService: TranslationModalService,
                injector: Injector) {
        super(injector);
    }

    ngOnInit() {
        this.routeSubscription = this.route.params.subscribe(params => this.onRouteChanged(params['id']));
        this.translationModalResponseSubscription = this.translationModalService.translationModalResponseObservable
            .subscribe(response => {
                if (response.success) {
                    // this.translationModalService.updateVocabWordInCollection(response.vocabWord, this.vocabWords);
                } else {
                    this.displayErrors(response.errors);
                }
            });
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

    public editMaterial(): void {
        this.mode = MaterialMode.Edit;
    }

    public deleteMaterial(): void {
        abp.ui.setBusy('body');
        /*this.materialService.deleteMaterial(this.material.id).then(response => {
            abp.ui.clearBusy('body');
            if (response.success) {
                this.router.navigateByUrl('materials');
            } else {
                response.errors.forEach(err => this.displayError(err));
            }
        });*/
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
                        this.router.navigateByUrl('material/' + response.id);
                    } else {
                        this.mode = MaterialMode.Read;
                    }
                });
        }
    }

    get materialStats(): MaterialStats[] {
        const stats: MaterialStats[] = [];

        const totalCount = this.material.words
            .map(w => w.count).reduce((pre, curr) => pre + curr, 0);

        const uniqueCount = this.vocabWords.length;
        this.pushStatToStats(stats, 'Total words', totalCount.toString());
        this.pushStatToStats(stats, 'Unique words', uniqueCount.toString());

        /*if (this.accountInformation.account.externalId !== 0) {
            const learnCount = this.vocabWords.filter(v => v.type === VocabType.LearnWord).length;
            const knownCount = this.vocabWords.filter(v => v.type === VocabType.KnownWord).length;

            this.pushStatToStats(stats, 'Learn words', learnCount.toString());
            this.pushStatToStats(stats, 'Known words', knownCount.toString());
            this.pushStatToStats(stats, 'Unsigned words', (uniqueCount - (learnCount + knownCount)).toString());
        }*/

        return stats;
    }

    get isEditButtonsVisible(): boolean {
        /*if (!this.accountInformation || !this.accountInformation.account || !this.material.owner) {
            return false;
        }*/

        /*return this.material.owner.externalId === this.accountInformation.account.externalId || this.accountInformation.isAdmin;*/
        return false;
    }

    get isAddToFavoritesButtonVisible(): boolean {
        /*if (!this.accountInformation || !this.accountInformation.account || this.mode === MaterialMode.Add) {
            return false;
        }*/
        return true;
    }

    public validationErrors(state: NgModel): string[] {
        return ComponentValidation.validationErrors(state);
    }

    private pushStatToStats(stats: MaterialStats[], name: string, value: string) {
        stats.push({
            name: name,
            value: value
        });
    }

    private initializeMaterial(id: number): void {
        abp.ui.setBusy('body');

        this.materialService.getMaterial(id).subscribe(response => {
            this.mode = MaterialMode.Read;
            this.material = response.material;
            this.vocabWords = response.vocabWords;

            if (this.appSession.getShownLoginName()) {
                /*this.favoriteMaterialsService.get(this.material.id).then(res => {
                    abp.ui.clearBusy('body');
                    if (res.success) {
                        this.isFavorite = res.isFavorite;
                    } else {
                        this.displayErrors(res.errors);
                    }
                });*/
                abp.ui.clearBusy('body');
            } else {
                abp.ui.clearBusy('body');
            }
        });
    }

    public removeFromFavorites(): void {
        abp.ui.setBusy('body');
        this.favoriteMaterialsService.delete(this.material.id).then(resp => {
            abp.ui.clearBusy('body');
            if (resp.success) {
                this.isFavorite = !this.isFavorite;
            } else {
                this.displayErrors(resp.errors);
            }
        });
    }

    public addToFavorites(): void {
        abp.ui.setBusy('body');
        this.favoriteMaterialsService.add(this.material.id).then(resp => {
            abp.ui.clearBusy('body');
            if (resp.success) {
                this.isFavorite = !this.isFavorite;
            } else {
                this.displayErrors(resp.errors);
            }
        });
    }

    ngOnDestroy() {
        this.routeSubscription.unsubscribe();
        this.translationModalResponseSubscription.unsubscribe();
    }
}
