using Volo.Abp.Settings;

namespace GraphTask.Settings;

public class GraphTaskSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(GraphTaskSettings.MySetting1));
    }
}
