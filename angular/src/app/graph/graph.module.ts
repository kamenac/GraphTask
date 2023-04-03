import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GraphRoutingModule } from './graph-routing.module';
import { GraphComponent } from './graph.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    GraphComponent
  ],
  imports: [
    CommonModule,
    GraphRoutingModule,
    NgxDatatableModule,
    ThemeSharedModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class GraphModule { }
