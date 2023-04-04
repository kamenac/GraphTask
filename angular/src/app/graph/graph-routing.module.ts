import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GraphComponent } from './graph.component';
import { GraphDetailComponent } from './graph-detail.component';

const routes: Routes = [
  { path: '', component: GraphComponent },
  { path: 'detail/:graphId', component: GraphDetailComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GraphRoutingModule { }
