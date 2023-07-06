import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ContactComponent } from './components/contact.component';

const ROUTES = [
  {
    path: '',
    component: ContactComponent,
  },
  {
    path: '**',
    redirectTo: '',
  },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(ROUTES),
  ],
  exports: [RouterModule],
})
export class ContactRoutingModule {}
