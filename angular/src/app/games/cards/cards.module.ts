import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardsRoutingModule } from '@app/games/cards/cards-routing.module';
import { CardsComponent } from '@app/games/cards/cards.component';

@NgModule({
  imports: [CommonModule, CardsRoutingModule],
  declarations: [CardsComponent],
  providers: []
})
export class CardsModule {
}
