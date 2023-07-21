import { Component } from '@angular/core';
import { ImageView } from '../../shared/models/image-view.interface';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss'],
})
export class AboutComponent {

  teaserImages: ImageView[] = [
    { url: 'assets/projects/comanche-av.png', title: 'Comanche', caption: undefined },
    { url: 'assets/projects/menu-griddler.png', title: 'Custom Elements - Menu', caption: undefined },
    { url: 'assets/projects/fluent-errors.png', title: 'Fluent Errors', caption: undefined },
    { url: 'assets/projects/griddler.gif', title: 'Griddler', caption: undefined },
  ]
}
