import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ProjectCryptoStreamComponent } from './components/project-crypto-stream.component';

const ROUTES = [
  {
    path: '',
    component: ProjectCryptoStreamComponent,
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
export class ProjectCryptoStreamRoutingModule {}
