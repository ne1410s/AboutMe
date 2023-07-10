import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProjectCustomElementsComponent } from './components/project-custom-elements.component';
import { ProjectCustomElementsRoutingModule } from './project-custom-elements-routing.module';

@NgModule({
  declarations: [
    ProjectCustomElementsComponent
  ],
  imports: [
    CommonModule,
    ProjectCustomElementsRoutingModule,
  ],
  exports: [
    ProjectCustomElementsComponent
  ],
})
export class ProjectCustomElementsModule {}
