import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ProjectGriddlerComponent } from './components/project-griddler.component';

const ROUTES = [
  {
    path: '',
    component: ProjectGriddlerComponent,
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
export class ProjectGriddlerRoutingModule {}
