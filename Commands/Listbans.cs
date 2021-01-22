using CommandSystem;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.IO;

namespace Easy_Unban
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class Listbans : ICommand
    {

            
        public string[] Aliases { get; } = plugin.Singleton.Config.ListBansCmdAliases;

        public string Description { get; } = plugin.Singleton.Config.ListBansCmdInfo;

        string ICommand.Command { get; } = plugin.Singleton.Config.ListBansCmd;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = plugin.Singleton.Config.ListBansCmdShowingListMsg;
            if (plugin.IdBanlist != null)
            {
                response += "\n" + plugin.Singleton.Config.ListBansCmdShowingIdList;
                for (int i = 0; i < plugin.IdBanlist.Count; i++)
                {
                    string message = plugin.Singleton.Config.ListBansCmdBanList.Replace("{BanNumber}", plugin.IdBanlist[i].Id.ToString()).Replace("{PlayerName}", plugin.IdBanlist[i].Name);
                    response += "\n" + message;
                }
            }
            else response += "\n" + plugin.Singleton.Config.NoIpORIdbannedUserFound.Replace("{BanType}", "UserId");
            if (plugin.IpBanlist != null)
            {
                response += "\n---Ip banuri---";
                for (int i = 0; i < plugin.IpBanlist.Count; i++)
                {
                    string message = plugin.Singleton.Config.ListBansCmdBanList.Replace("{BanNumber}", plugin.IpBanlist[i].Id.ToString()).Replace("{PlayerName}", plugin.IpBanlist[i].Name);
                    response += "\n" + message;
                }
            }
            else response += "\n" + plugin.Singleton.Config.NoIpORIdbannedUserFound.Replace("{BanType}", "Ip");

            return true;

        }
    }
}
