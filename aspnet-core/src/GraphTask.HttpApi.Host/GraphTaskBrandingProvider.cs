using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace GraphTask;

[Dependency(ReplaceServices = true)]
public class GraphTaskBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "GraphTask";
}
