import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ProjectCustomElementsComponent } from './components/project-custom-elements.component';

const ROUTES = [
  {
    path: '',
    component: ProjectCustomElementsComponent,
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
export class ProjectCustomElementsRoutingModule {}
