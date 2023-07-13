import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ProjectComancheComponent } from './components/project-comanche.component';

const ROUTES = [
  {
    path: '',
    component: ProjectComancheComponent,
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
export class ProjectComancheRoutingModule {}
