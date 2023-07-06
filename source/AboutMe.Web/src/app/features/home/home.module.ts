import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { HomeComponent } from './components/home.component';
import { HomeRoutingModule } from './home-routing.module';
import { MilkComponent } from './components/milk/milk.component';

@NgModule({
  declarations: [
    HomeComponent,
    MilkComponent,
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
  ],
  exports: [
    HomeComponent,
    MilkComponent,
  ],
})
export class HomeModule {}
