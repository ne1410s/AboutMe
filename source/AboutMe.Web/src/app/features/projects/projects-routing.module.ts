import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ProjectsPageComponent } from './components/projects-page/projects-page.component';

const ROUTES = [
  {
    path: '',
    component: ProjectsPageComponent
  },
  {
    path: 'acme',
    loadChildren: () => import('./project-acme/project-acme.module').then(m => m.ProjectAcmeModule)
  },
  {
    path: 'comanche',
    loadChildren: () => import('./project-comanche/project-comanche.module').then(m => m.ProjectComancheModule)
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
    path: 'fluent-errors',
    loadChildren: () => import('./project-fluent-errors/project-fluent-errors.module').then(m => m.ProjectFluentErrorsModule)
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
