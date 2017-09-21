import { NgForm, NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { MaterialService } from './material.service';
import { Material as MaterialModel, MaterialMode, MaterialStats, VocabType, VocabWordFiltration } from './material.models';
import { VocabWord } from './material.models';
import { SpinnerService } from '../global/spinner/spinner.service';
import { TranslationModalService } from '../global/components/translation-modal/translation-modal.service';
import { BaseComponent } from '../global/base-component';
import { ComponentValidation } from '../global/component-validation';
import { UserModel } from '../auth/auth.models';
import { UserService } from '../auth/user.service';

@Component({
    templateUrl: 'app/material/material.template.html'
})

export class MaterialComponent extends BaseComponent implements OnInit, OnDestroy {
    public mode: MaterialMode = null;
    public vocabWords: VocabWord[] = [];
    public material: MaterialModel = new MaterialModel();
    public userModel: UserModel;
    public formSubmited = false;
    public filtration: VocabWordFiltration = new VocabWordFiltration();
    private routeSubscription: ISubscription;
    private userSubscription: ISubscription;
    private translationModalResponseSubscription: ISubscription;

    constructor(private materialService: MaterialService, private route: ActivatedRoute, private router: Router,
        private spinner: SpinnerService, private translationModalService: TranslationModalService,
        private userService: UserService) {
        super();
    }

    ngOnInit() {
        this.userSubscription = this.userService.getUserObservable().subscribe(user => {
            this.userModel = user;
        });

        this.routeSubscription = this.route.params.subscribe(params => this.onRouteChanged(params['id']));
        this.translationModalResponseSubscription = this.translationModalService.translationModalResponseObserverable
            .subscribe(response => {
                if (response.success) {
                    this.translationModalService.updateVocabWordInCollection(response.vocabWord, this.vocabWords);
                } else {
                    this.displayErrors(response.errors);
                }
            });
    }

    private onRouteChanged(param: string): void {
        if (param === 'create') {
            this.mode = MaterialMode.Add;
            this.material = new MaterialModel();
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
        this.spinner.displaySpinner(true);
        this.materialService.deleteMaterial(this.material.id).then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                this.router.navigateByUrl('materials');
            } else {
                response.errors.forEach(err => this.displayError(err, 'Delete material error'));
            }
        });
    }

    public saveMaterial(form: NgForm): void {
        this.formSubmited = true;
        if (form.valid) {
            this.spinner.displaySpinner(true);
            this.materialService.saveMaterial(this.material, this.vocabWords).then(response => {
                this.spinner.displaySpinner(false);
                if (response.success) {
                    if (this.mode == MaterialMode.Add) {
                        this.router.navigateByUrl('material/' + response.id);
                    } else {
                        this.mode = MaterialMode.Read;
                    }
                } else {
                    response.errors.forEach(err => this.displayError(err, 'Save material error'));
                }
            });
            this.formSubmited = false;
        }
    }

    get materialStats(): MaterialStats[] {
        let stats: MaterialStats[] = [];

        var totalCount = this.material.words
            .map(w => w.count).reduce((pre, curr) => pre + curr, 0);

        var uniqueCount = this.vocabWords.length;
        this.pushStatToStats(stats, 'Total words', totalCount.toString());
        this.pushStatToStats(stats, 'Unique words', uniqueCount.toString());

        if (this.userModel.isLoggedIn) {
            var learnCount = this.vocabWords.filter(v => v.type === VocabType.LearnWord).length;
            var knownCount = this.vocabWords.filter(v => v.type === VocabType.KnownWord).length;

            this.pushStatToStats(stats, 'Learn words', learnCount.toString());
            this.pushStatToStats(stats, 'Known words', knownCount.toString());
            this.pushStatToStats(stats, 'Unsigned words', (uniqueCount - (learnCount + knownCount)).toString());
        }

        return stats;
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
        this.spinner.displaySpinner(true);

        this.materialService.getMaterial(id).then(response => {
            this.spinner.displaySpinner(false);
            if (response.success) {
                this.mode = MaterialMode.Read;
                this.material = response.material;
                this.vocabWords = response.vocabWords;
            } else {
                this.mode = null;
                response.errors.forEach(err => this.displayError(err, 'Initialize material error'));
            }
        });
    }

    ngOnDestroy() {
        this.routeSubscription.unsubscribe();
        this.userSubscription.unsubscribe();
        this.translationModalResponseSubscription.unsubscribe();
    }
}