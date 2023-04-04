import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { GraphService } from '@proxy/graph';
import { GraphDto } from '@proxy/graph/models';
import { ActivatedRoute, Params } from '@angular/router';
import { DecimalPipe } from '@angular/common';
import { Orb } from '@memgraph/orb';


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

      this.graphService.get(this.graphId)
        .subscribe(result => {
          this.graphDetail = result;
        });
    });
  }

  loadVisualization(): void {
    this.graphService.getWholeGraph(this.graphId)
      .subscribe(result => {
        this.graphDetail = result;

        this.initializeOrb();
      });
  }


  initializeOrb(): void {
    const container = document.getElementById('orbGraph');

    const nodes = [];
    const edges = [];

    console.info("Building nodes and edges list");

    this.graphDetail.nodes.forEach(e => {
      nodes.push({ id: e, label: e.toString() });
    });

    let edgeId = 0;
    this.graphDetail.edges.forEach(e => {
      edges.push({ id: edgeId, start: e.start, end: e.end });
      edgeId++;
    });

    console.info("Done");


    const orb = new Orb(container);

    // Initialize nodes and edges
    orb.data.setup({ nodes, edges });

    orb.view.render(() => {
      orb.view.recenter();
    });

    // const nodes: MyNode[] = [
    //   { id: 1, label: 'Orb' },
    //   { id: 2, label: 'Graph' },
    //   { id: 3, label: 'Canvas' },
    // ];
    // const edges: MyEdge[] = [
    //   { id: 1, start: 1, end: 2, label: 'DRAWS' },
    //   { id: 2, start: 2, end: 3, label: 'ON' },
    // ];

    // const orb = new Orb<MyNode, MyEdge>(container);

    // // Initialize nodes and edges
    // orb.data.setup({ nodes, edges });

    // // Render and recenter the view
    // orb.view.render(() => {
    //   orb.view.recenter();
    // });

  }

}