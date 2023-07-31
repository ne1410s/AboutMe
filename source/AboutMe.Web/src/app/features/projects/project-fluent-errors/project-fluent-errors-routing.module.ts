import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ProjectFluentErrorsComponent } from './components/project-fluent-errors.component';

const ROUTES = [
  {
    path: '',
    component: ProjectFluentErrorsComponent,
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
export class ProjectFluentErrorsRoutingModule {}
