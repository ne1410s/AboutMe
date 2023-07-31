import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ProjectLibraComponent } from './components/project-libra.component';

const ROUTES = [
  {
    path: '',
    component: ProjectLibraComponent,
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
export class ProjectLibraRoutingModule {}
