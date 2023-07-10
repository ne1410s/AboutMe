import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProjectPsrComponent } from './components/project-psr.component';
import { ProjectPsrRoutingModule } from './project-psr-routing.module';

@NgModule({
  declarations: [
    ProjectPsrComponent
  ],
  imports: [
    CommonModule,
    ProjectPsrRoutingModule,
  ],
  exports: [
    ProjectPsrComponent
  ],
})
export class ProjectPsrModule {}
