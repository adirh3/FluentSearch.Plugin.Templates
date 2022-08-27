using Blast.API.Settings;
using Blast.Core.Objects;

namespace Template.Fluent.Plugin;

public class MySearchAppSettings : SearchApplicationSettingsPage
{
    public MySearchAppSettings(SearchApplicationInfo searchApplicationInfo) : base(searchApplicationInfo)
    {
    }

    /// <summary>
    /// A text (text box) setting that can be set by the user in the Settings page
    /// </summary>
    [Setting(Name = nameof(MyTextSetting), DisplayedName = "My text setting", DefaultValue = "Default value")]
    public string MyTextSetting { get; set; } = "Default value";

    /// <summary>
    /// A boolean (toggle) setting that can be set by the user in the Settings page
    /// </summary>
    [Setting(Name = nameof(MyBooleanSetting), DisplayedName = "My boolean setting")]
    public bool MyBooleanSetting { get; set; }
}