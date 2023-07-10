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
    path: 'custom-elements',
    loadChildren: () => import('./project-custom-elements/project-custom-elements.module').then(m => m.ProjectCustomElementsModule)
  },
  {
    path: 'griddler',
    loadChildren: () => import('./project-griddler/project-griddler.module').then(m => m.ProjectGriddlerModule)
  },
  {
    path: 'libra',
    loadChildren: () => import('./project-libra/project-libra.module').then(m => m.ProjectLibraModule)
  },
  {
    path: 'psr',
    loadChildren: () => import('./project-psr/project-psr.module').then(m => m.ProjectPsrModule)
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
