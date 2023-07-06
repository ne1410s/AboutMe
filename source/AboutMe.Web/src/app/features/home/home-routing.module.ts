import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { HomeComponent } from './components/home.component';
import { MilkComponent } from './components/milk/milk.component';

const ROUTES = [
  {
    path: '',
    component: HomeComponent,
    children: [
      { path: 'milk', component: MilkComponent }
    ]
  },
  {
    path: '**',
    redirectTo: '',
  },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(ROUTES),
  ],
  exports: [RouterModule],
})
export class HomeRoutingModule {}
