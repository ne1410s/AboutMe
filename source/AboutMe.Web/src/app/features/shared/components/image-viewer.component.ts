import { Component, Input } from '@angular/core';
import { ImageView } from '../models/image-view.interface';

@Component({
    selector: 'app-image-viewer',
    templateUrl: './image-viewer.component.html',
    styleUrls: ['./image-viewer.component.scss'],
    standalone: false
})
export class ImageViewerComponent {
  @Input()
  images!: ImageView[];

  fullScreen: boolean = false;

  index: number = 0;

  toggleFullScreen(): void {
    this.setIndex(0);
    this.fullScreen = !this.fullScreen;
  }

  setIndex(index: number): void {
    this.index = Math.max(0, Math.min(this.images.length - 1, index));
  }

  getFullScreenX(): string {
    return `translateX(${this.index * -100}vw)`;
  }

  expandToImage(index: number): void {
    if (!this.fullScreen) {
      this.toggleFullScreen();
      this.setIndex(index);
    }
  }

  viewerClick(event: MouseEvent) {
    const isCloseTarget = (event.target as any).classList.contains('closer');
    if (this.fullScreen && isCloseTarget) {
      this.fullScreen = false;
    }
  }
}
