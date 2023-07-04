import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { HomeComponent } from './features/home/components/home.component';
import { HomeModule } from './features/home/home.module';

const APP_ROUTES = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: '**',
    redirectTo: '',
  },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(APP_ROUTES, {
      onSameUrlNavigation: 'reload',
      scrollPositionRestoration: 'enabled',
    }),
    HomeModule,
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
