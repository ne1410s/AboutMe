import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuPageComponent } from './components/menu-page/menu-page.component';

const ROUTES = [
  {
    path: '',
    component: MenuPageComponent,
    children: [
      { 
        path: 'contact',
        loadChildren: () => import('../contact/contact.module').then(m => m.ContactModule)
      }
    ]
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
export class MenuRoutingModule {}
