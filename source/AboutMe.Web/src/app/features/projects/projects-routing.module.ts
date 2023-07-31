import { CommonModule } from '@angular/common';
import { NgModule, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterModule, Routes } from '@angular/router';

import { ProjectsPageComponent } from './components/projects-page/projects-page.component';
import { ProjectMetadataService } from './services/project-metadata.service';

const canActivateProject: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const projectPath = route.routeConfig?.path || '';
  const retVal = inject(ProjectMetadataService).isEnabled(projectPath);
  return retVal || inject(Router).navigate(['/projects']);
};

const ROUTES: Routes = [
  {
    path: '',
    component: ProjectsPageComponent,
  },
  {
    path: 'acme',
    loadChildren: () => import('./project-acme/project-acme.module').then((m) => m.ProjectAcmeModule),
    canActivate: [canActivateProject],
  },
  {
    path: 'comanche',
    loadChildren: () => import('./project-comanche/project-comanche.module').then((m) => m.ProjectComancheModule),
    canActivate: [canActivateProject],
  },
  {
    path: 'crypto-stream',
    loadChildren: () =>
      import('./project-crypto-stream/project-crypto-stream.module').then((m) => m.ProjectCryptoStreamModule),
    canActivate: [canActivateProject],
  },
  {
    path: 'custom-elements',
    loadChildren: () =>
      import('./project-custom-elements/project-custom-elements.module').then((m) => m.ProjectCustomElementsModule),
    canActivate: [canActivateProject],
  },
  {
    path: 'fluent-errors',
    loadChildren: () =>
      import('./project-fluent-errors/project-fluent-errors.module').then((m) => m.ProjectFluentErrorsModule),
    canActivate: [canActivateProject],
  },
  {
    path: 'griddler',
    loadChildren: () => import('./project-griddler/project-griddler.module').then((m) => m.ProjectGriddlerModule),
    canActivate: [canActivateProject],
  },
  {
    path: 'libra',
    loadChildren: () => import('./project-libra/project-libra.module').then((m) => m.ProjectLibraModule),
    canActivate: [canActivateProject],
  },
  {
    path: 'psr',
    loadChildren: () => import('./project-psr/project-psr.module').then((m) => m.ProjectPsrModule),
    canActivate: [canActivateProject],
  },
  {
    path: '**',
    redirectTo: '',
  },
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(ROUTES)],
  exports: [RouterModule],
})
export class ProjectsRoutingModule {}
