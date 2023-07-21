import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { ImageViewerComponent } from './components/image-viewer.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    ImageViewerComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
  ],
  exports: [
    ImageViewerComponent,
  ],
})
export class SharedModule {}
