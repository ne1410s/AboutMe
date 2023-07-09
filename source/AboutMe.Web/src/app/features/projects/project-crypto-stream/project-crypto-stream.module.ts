import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProjectCryptoStreamComponent } from './components/project-crypto-stream.component';
import { ProjectCryptoStreamRoutingModule } from './project-crypto-stream-routing.module';

@NgModule({
  declarations: [
    ProjectCryptoStreamComponent
  ],
  imports: [
    CommonModule,
    ProjectCryptoStreamRoutingModule
  ],
  exports: [
    ProjectCryptoStreamComponent
  ],
})
export class ProjectCryptoStreamModule {}
