import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProjectsRoutingModule } from './projects-routing.module';
import { ProjectsPageComponent } from './components/projects-page/projects-page.component';
import { ProjectsTableComponent } from './components/projects-table/projects-table.component';

@NgModule({
  declarations: [ProjectsPageComponent, ProjectsTableComponent],
  imports: [CommonModule, ProjectsRoutingModule],
  exports: [ProjectsPageComponent, ProjectsTableComponent],
})
export class ProjectsModule {}
