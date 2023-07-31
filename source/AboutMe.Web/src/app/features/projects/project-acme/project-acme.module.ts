import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProjectAcmeComponent } from './components/project-acme.component';
import { ProjectAcmeRoutingModule } from './project-acme-routing.module';

@NgModule({
  declarations: [ProjectAcmeComponent],
  imports: [CommonModule, ProjectAcmeRoutingModule],
  exports: [ProjectAcmeComponent],
})
export class ProjectAcmeModule {}
