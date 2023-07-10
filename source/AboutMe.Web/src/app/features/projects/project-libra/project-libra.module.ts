import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProjectLibraComponent } from './components/project-libra.component';
import { ProjectLibraRoutingModule } from './project-libra-routing.module';

@NgModule({
  declarations: [
    ProjectLibraComponent
  ],
  imports: [
    CommonModule,
    ProjectLibraRoutingModule,
  ],
  exports: [
    ProjectLibraComponent
  ],
})
export class ProjectLibraModule {}
