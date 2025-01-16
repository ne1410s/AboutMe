import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { ImageViewerComponent } from './components/image-viewer.component';
import { CommonModule } from '@angular/common';

@NgModule({ declarations: [ImageViewerComponent],
    exports: [ImageViewerComponent], imports: [CommonModule], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class SharedModule {}
