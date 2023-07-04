import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './features/shared/shared.module';
import { EnvServiceProvider } from './features/shared/services/env.service';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
  ],
  providers: [
    EnvServiceProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
