using Dalamud.Game.ClientState;
using Dalamud.Game.Gui;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Utility;

namespace Cid
{
    public sealed class CidPlugin : IDalamudPlugin
    {
        private ICommandManager Commands { get; }
        private ClientState Client { get; }
        private ChatGui Chat { get; }

        public string Name => "Cid";

        public CidPlugin(ICommandManager commands, ClientState client, ChatGui chat)
        {
            this.Commands = commands;
            this.Client = client;
            this.Chat = chat;

            this.Commands.AddHandler("/cid", new(this.CidCommandHandler));
        }

        public void CidCommandHandler(string name, string args)
        {
            var playerName = this.Client.LocalPlayer!.Name;
            var world = this.Client.LocalPlayer.HomeWorld.GameData!.Name.ToDalamudString().TextValue;
            var dc = this.Client.LocalPlayer.HomeWorld.GameData.DataCenter!.Value!.Name.ToDalamudString().TextValue;
            this.Chat.Print($"{this.Client.LocalContentId:X16}: {playerName} <{world}> [{dc}]");
        }

        public void Dispose()
        {
            this.Commands.RemoveHandler("/cid");
        }
    }
}