import type { IRemoteStreamContent } from '../volo/abp/content/models';
import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateGraphDto {
  name: string;
  content: IRemoteStreamContent;
}

export interface EdgeDto {
  end: number;
  start: number;
}

export interface GraphDto extends AuditedEntityDto<number> {
  name?: string;
  averageNumberOfAdjacentNodes: number;
  numberOfNodes: number;
  edges: EdgeDto[];
  nodes: number[];
}
