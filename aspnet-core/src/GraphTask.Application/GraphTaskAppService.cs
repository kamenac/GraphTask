using System;
using System.Collections.Generic;
using System.Text;
using GraphTask.Localization;
using Volo.Abp.Application.Services;

namespace GraphTask;

/* Inherit your application services from this class.
 */
public abstract class GraphTaskAppService : ApplicationService
{
    protected GraphTaskAppService()
    {
        LocalizationResource = typeof(GraphTaskResource);
    }
}
