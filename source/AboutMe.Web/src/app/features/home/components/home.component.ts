import { Component } from '@angular/core';

import { ApiService } from '../../shared/services/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {

  forecast$ = this.api.getForecast();

  constructor(private api: ApiService) {
  }
}
