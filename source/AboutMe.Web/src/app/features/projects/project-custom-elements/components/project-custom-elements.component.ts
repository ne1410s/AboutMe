import { Component, ElementRef, ViewChild } from '@angular/core';
import { Popup } from '@ne1410s/popup';

@Component({
    selector: 'app-project-custom-elements',
    templateUrl: './project-custom-elements.component.html',
    styleUrls: ['./project-custom-elements.component.scss'],
    standalone: false
})
export class ProjectCustomElementsComponent {
  @ViewChild('popup') popup!: ElementRef<Popup>;

  onItemSelect(event: any) {
    console.log(event.detail.title);
  }

  onButtonClick() {
    this.popup.nativeElement.open();
  }
}
