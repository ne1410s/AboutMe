import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProjectsRoutingModule } from './projects-routing.module';
import { ProjectSummaryComponent } from './components/project-summary.component';

@NgModule({
  declarations: [
    ProjectSummaryComponent
  ],
  imports: [
    CommonModule,
    ProjectsRoutingModule,
  ],
  exports: [
    ProjectSummaryComponent
  ],
})
export class ProjectsModule {}
