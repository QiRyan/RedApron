using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using RedApron.Windows;
using Dalamud.Game.ClientState.Party;

namespace RedApron
{
    public sealed class RedApronPlugin : IDalamudPlugin
    {
        public string Name => "Red Apron";

        private DalamudPluginInterface PluginInterface { get; init; }
        private CommandManager CommandManager { get; init; }
        public Configuration Configuration { get; init; }

        public WindowSystem WindowSystem = new("Red Apron");

        private RedApronWindow MainWindow { get; init; }

        public RedApronPlugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] CommandManager commandManager,
            [RequiredVersion("1.0")] PartyList partyMembers)
        {
            this.PluginInterface = pluginInterface;
            this.CommandManager = commandManager;

            this.Configuration = Configuration.Get(this.PluginInterface);

            // you might normally want to embed resources and load them from the manifest stream
            MainWindow = new RedApronWindow(this);

            WindowSystem.AddWindow(MainWindow);
            var command = new CommandInfo((_, _) =>
            {
                MainWindow.IsOpen ^= true;
                MainWindow.ShowRedApron ^= true;
            }) { HelpMessage = "Opens Red Apron window"};

            this.CommandManager.AddHandler("/redapron", command);
            this.CommandManager.AddHandler("/ra", command);

            this.PluginInterface.UiBuilder.Draw += DrawUI;
            this.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;
        }

        public void Dispose()
        {
            this.WindowSystem.RemoveAllWindows();
            MainWindow.Dispose();
            this.CommandManager.RemoveHandler("/redapron");
            this.CommandManager.RemoveHandler("/ra");
        }

        private void DrawUI()
        {
            this.WindowSystem.Draw();
        }

        public void DrawConfigUI()
        {
        }
    }
}
