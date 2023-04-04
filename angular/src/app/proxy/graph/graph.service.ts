import type { CreateGraphDto, GraphDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class GraphService {
  apiName = 'Default';
  

  create = (input: CreateGraphDto) =>
    this.restService.request<any, GraphDto>({
      method: 'POST',
      url: '/api/app/graph',
      params: { name: input.name },
    },
    { apiName: this.apiName });
  

  createWholeGraph = (input: CreateGraphDto) =>
    this.restService.request<any, number>({
      method: 'POST',
      url: '/api/app/graph/whole-graph',
      params: { name: input.name },
    },
    { apiName: this.apiName });
  

  delete = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/graph/${id}`,
    },
    { apiName: this.apiName });
  

  get = (id: number) =>
    this.restService.request<any, GraphDto>({
      method: 'GET',
      url: `/api/app/graph/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<GraphDto>>({
      method: 'GET',
      url: '/api/app/graph',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getWholeGraph = (id: number) =>
    this.restService.request<any, GraphDto>({
      method: 'GET',
      url: `/api/app/graph/${id}/whole-graph`,
    },
    { apiName: this.apiName });
  

  update = (id: number, input: CreateGraphDto) =>
    this.restService.request<any, GraphDto>({
      method: 'PUT',
      url: `/api/app/graph/${id}`,
      params: { name: input.name },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
