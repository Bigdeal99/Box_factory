import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePage } from './home.page';
import {BoxPageComponent} from "./box-page/box-page.component";



const routes: Routes = [
  {
    path: '',
    component: HomePage,

  },

  {
    path: ':id',
    component: BoxPageComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomePageRoutingModule {}
