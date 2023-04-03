using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Content;

namespace GraphTask.Graph
{
    public class CreateGraphDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// file content sent from the client
        /// </summary>
        public IRemoteStreamContent Content { get; set; }
    }
}
