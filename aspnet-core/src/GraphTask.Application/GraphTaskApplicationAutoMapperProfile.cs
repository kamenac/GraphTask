using AutoMapper;
using GraphTask.Graph;

namespace GraphTask;

public class GraphTaskApplicationAutoMapperProfile : Profile
{
    public GraphTaskApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Graph.Graph, GraphDto>();
        CreateMap<CreateGraphDto, Graph.Graph>();
    }
}
