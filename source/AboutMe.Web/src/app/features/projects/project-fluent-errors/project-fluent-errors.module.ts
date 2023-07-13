import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProjectFluentErrorsComponent } from './components/project-fluent-errors.component';
import { ProjectFluentErrorsRoutingModule } from './project-fluent-errors-routing.module';

@NgModule({
  declarations: [
    ProjectFluentErrorsComponent
  ],
  imports: [
    CommonModule,
    ProjectFluentErrorsRoutingModule,
  ],
  exports: [
    ProjectFluentErrorsComponent
  ],
})
export class ProjectFluentErrorsModule {}
