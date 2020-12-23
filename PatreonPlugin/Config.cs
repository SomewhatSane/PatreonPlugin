using Exiled.API.Interfaces;
using System.ComponentModel;

namespace PatreonPlugin
{
    public class Config : IConfig
    {
        [Description("Indicates whether or not the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Check for updates on plugin load.")]
        public bool CheckForUpdates { get; set; } = true;
    }
}
