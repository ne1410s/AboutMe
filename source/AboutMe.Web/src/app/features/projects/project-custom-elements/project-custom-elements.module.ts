import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';

import { ProjectCustomElementsComponent } from './components/project-custom-elements.component';
import { ProjectCustomElementsRoutingModule } from './project-custom-elements-routing.module';

@NgModule({
  declarations: [ProjectCustomElementsComponent],
  imports: [CommonModule, ProjectCustomElementsRoutingModule],
  exports: [ProjectCustomElementsComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ProjectCustomElementsModule {}
