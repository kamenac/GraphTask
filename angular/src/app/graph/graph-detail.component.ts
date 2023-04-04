import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { GraphService } from '@proxy/graph';
import { GraphDto } from '@proxy/graph/models';
import { ActivatedRoute, Params } from '@angular/router';
import { DecimalPipe } from '@angular/common';


@Component({
  selector: 'app-graph-detail',
  templateUrl: './graph-detail.component.html',
  providers: [ListService],
})


export class GraphDetailComponent implements OnInit {
  graphId: number;
  graphDetail: GraphDto;

  constructor(
    private graphService: GraphService,
    private activatedRoute: ActivatedRoute
  ) {
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.graphId = params['graphId'];
      this.graphService.getWholeGraph(this.graphId)
        .subscribe(result => {
          this.graphDetail = result;
        });
    });


  }

}