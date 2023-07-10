import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProjectGriddlerComponent } from './components/project-griddler.component';
import { ProjectGriddlerRoutingModule } from './project-griddler-routing.module';

@NgModule({
  declarations: [
    ProjectGriddlerComponent
  ],
  imports: [
    CommonModule,
    ProjectGriddlerRoutingModule,
  ],
  exports: [
    ProjectGriddlerComponent
  ],
})
export class ProjectGriddlerModule {}
