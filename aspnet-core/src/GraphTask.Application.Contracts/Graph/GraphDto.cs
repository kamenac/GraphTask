using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Volo.Abp.Application.Dtos;

namespace GraphTask.Graph
{
    public class GraphDto : AuditedEntityDto<int>
    {
        public string Name { get; set; }

    }
}
