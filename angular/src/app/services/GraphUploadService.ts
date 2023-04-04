
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class GraphUploadService {
    apiName = 'Default';

    // hand-written to fix the proxy generated code
    createWholeGraph = (name: string, formData) =>
        this.restService.request<any, number>({
            method: 'POST',
            url: '/api/app/graph/whole-graph',
            params: { name: name },
            body: formData
        },
            { apiName: this.apiName });

    constructor(private restService: RestService) { }
}
