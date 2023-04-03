using GraphTask.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace GraphTask.Permissions;

public class GraphTaskPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(GraphTaskPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(GraphTaskPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<GraphTaskResource>(name);
    }
}
