import { Component } from '@angular/core';
import { ApiService } from '../../shared/services/api.service';

@Component({
    selector: 'app-contact',
    templateUrl: './contact.component.html',
    styleUrls: ['./contact.component.scss'],
    standalone: false
})
export class ContactComponent {
  forecast$ = this.api.getForecast();

  constructor(private api: ApiService) {}
}
