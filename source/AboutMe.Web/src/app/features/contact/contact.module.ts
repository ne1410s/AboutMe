import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ContactComponent } from './components/contact.component';
import { ContactRoutingModule } from './contact-routing.module';

@NgModule({
  declarations: [ContactComponent],
  imports: [CommonModule, ContactRoutingModule],
  exports: [ContactComponent],
})
export class ContactModule {}
