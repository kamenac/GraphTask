﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace GraphTask.Graph
{
    public interface IGraphAppService : ICrudAppService<
       GraphDto,
       int, // Primary key type
       PagedAndSortedResultRequestDto,
       CreateGraphDto>, ITransientDependency
    {
        /// <summary>
        /// Creates a graph from CreateGraphDto input and stores it to database
        /// </summary>
        /// <param name="input">CreateGraphDto</param>
        /// <returns>Graph entity id</returns>
        Task<int> CreateGraphAsync(CreateGraphDto input);
    }
}