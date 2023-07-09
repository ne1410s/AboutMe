import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ProjectSummaryComponent } from './components/project-summary.component';

const ROUTES = [
  {
    path: '',
    component: ProjectSummaryComponent
  },
  {
    path: 'acme',
    loadChildren: () => import('./project-acme/project-acme.module').then(m => m.ProjectAcmeModule)
  },
  {
    path: 'crypto-stream',
    loadChildren: () => import('./project-crypto-stream/project-crypto-stream.module').then(m => m.ProjectCryptoStreamModule)
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
export class ProjectsRoutingModule {}
