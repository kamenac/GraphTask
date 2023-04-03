import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { GraphService } from '@proxy/graph';
import { GraphDto } from '@proxy/graph/models';
import { FormGroup, FormBuilder, FormsModule, Validators } from '@angular/forms';


@Component({
  selector: 'app-graph',
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.scss'],
  providers: [ListService],
})
export class GraphComponent implements OnInit {
  dataset = { items: [], totalCount: 0 } as PagedResultDto<GraphDto>;
  isModalOpen = false;
  form: FormGroup;

  constructor(
    public readonly list: ListService,
    private graphService: GraphService,
    private fb: FormBuilder // inject FormBuilder
  ) {
  }

  ngOnInit() {
    const datasetStreamCreator = (query) => this.graphService.getList(query);

    this.list.hookToQuery(datasetStreamCreator).subscribe((response) => {
      this.dataset = response;
    });
  }

  createDataset() {
    this.isModalOpen = true;
    this.buildForm();
  }

  buildForm() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      content: ['', Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    debugger;

    this.graphService.createGraph(this.form.value).subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }


}
