import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { EnvService } from '../../shared/services/env.service';
import { Forecast } from '../models/forecast.interface';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private readonly baseUrl = `${this.env.apiUrl}`;

  constructor(
    private http: HttpClient,
    private env: EnvService
  ) {}

  getForecast(): Observable<Forecast> {
    return this.http.get<Forecast>(`${this.baseUrl}/forecasts`);
  }
}
