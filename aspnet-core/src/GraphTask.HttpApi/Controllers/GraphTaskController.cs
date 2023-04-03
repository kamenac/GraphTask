using GraphTask.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace GraphTask.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class GraphTaskController : AbpControllerBase
{
    protected GraphTaskController()
    {
        LocalizationResource = typeof(GraphTaskResource);
    }
}
