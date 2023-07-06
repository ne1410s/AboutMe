import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { MenuRoutingModule } from './menu-routing.module';
import { MenuPageComponent } from './components/menu-page/menu-page.component';
import { MenuComponent } from './components/menu/menu.component';

@NgModule({
  declarations: [
    MenuPageComponent,
    MenuComponent,
  ],
  imports: [
    CommonModule,
    MenuRoutingModule,
  ],
  exports: [
    MenuPageComponent,
    MenuComponent,
  ],
})
export class MenuModule {}
