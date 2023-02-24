using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using ImGuiScene;

namespace RedApron.Windows;

public class RedApronWindow : Window, IDisposable
{
    private readonly RedApronPlugin Plugin;
    public bool ShowRedApron { get; internal set; }

    public RedApronWindow(RedApronPlugin plugin) : base("Red Apron", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
        this.Plugin = plugin;
    }

    public void Dispose()
    {
    }

    public override void Draw()
    {
        try
        {
            if (!ShowRedApron)
                return;

            var bShowRedApron = ShowRedApron;

            if (ImGui.Begin("Red Apron", ref bShowRedApron))
            {
                if(ImGui.BeginTable("deathrecap", 6,
                        ImGuiTableFlags.Borders | ImGuiTableFlags.NoBordersInBody | ImGuiTableFlags.ScrollY | ImGuiTableFlags.Resizable | ImGuiTableFlags.Reorderable |
                        ImGuiTableFlags.Hideable)) {
                    ImGui.TableSetupColumn("Time", ImGuiTableColumnFlags.WidthFixed);
                    ImGui.TableSetupColumn("Amount", ImGuiTableColumnFlags.WidthFixed);
                    ImGui.TableSetupColumn("Ability");
                    ImGui.TableSetupColumn("Source");
                    ImGui.TableSetupColumn("HP Before");
                    ImGui.TableSetupColumn("Status Effects");
                    ImGui.TableHeadersRow();

                    ImGui.TableNextRow();
                    ImGui.TableNextColumn();

                    ImGui.AlignTextToFramePadding();
                    ImGui.TextUnformatted("Hello");

                    ImGui.End();

                }
            }
        } catch (Exception ex) {
            ImGui.Text("Something went wrong!");
        }
    }
}
