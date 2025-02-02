using Dalamud.Configuration;
using Dalamud.Plugin;
using System;

namespace RedApron
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 0;

        public bool SomePropertyToBeSavedAndWithADefault { get; set; } = true;

        // the below exist just to make saving less cumbersome
        [NonSerialized] private DalamudPluginInterface PluginInterface = null!;

        public static Configuration Get(DalamudPluginInterface pluginInterface)
        {
            var config = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            config.PluginInterface = pluginInterface;
            return config;
        }
        public void Save()
        {
            this.PluginInterface!.SavePluginConfig(this);
        }
    }
}
