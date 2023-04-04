import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { GraphService } from '@proxy/graph';
import { GraphDto, CreateGraphDto } from '@proxy/graph/models';
import { FormGroup, FormBuilder, FormsModule, Validators } from '@angular/forms';
import { GraphUploadService } from '../services/GraphUploadService';


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
  fileToUpload: File;

  constructor(
    public readonly list: ListService,
    private graphService: GraphService,
    private graphUploadService: GraphUploadService,
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

  openDetail(detail: GraphDto) {
    debugger;
    alert(detail.id);
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    let formData: FormData = new FormData();
    formData.append('content', this.fileToUpload, this.fileToUpload.name);

    this.graphUploadService.createWholeGraph(this.form.value["name"], formData).subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }


}
