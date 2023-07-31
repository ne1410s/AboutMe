import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProjectComancheComponent } from './components/project-comanche.component';
import { ProjectComancheRoutingModule } from './project-comanche-routing.module';

@NgModule({
  declarations: [ProjectComancheComponent],
  imports: [CommonModule, ProjectComancheRoutingModule],
  exports: [ProjectComancheComponent],
})
export class ProjectComancheModule {}
