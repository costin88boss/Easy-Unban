using CommandSystem;
using Exiled.API.Features;
using Easy_Unban;
using System;
using System.Collections.Generic;
using System.IO;

namespace Easy_UnBan.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class ReloadBans : ICommand
    {
        public string Command { get; } = plugin.Singleton.Config.ReloadBansCmd;

        public string[] Aliases { get; } = plugin.Singleton.Config.ReloadBansCmdAliases;

        public string Description { get; } = plugin.Singleton.Config.ReloadBansCmdInfo;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            plugin.IpBanlist = new List<UserBans>();
            plugin.IdBanlist = new List<UserBans>();

            bool Ipbans = true;
            bool Idbans = true;

            string[] IpBansTxt = File.ReadAllLines(plugin.Singleton.Config.ServerDir + @"\IpBans.txt");
            string[] IdBansTxt = File.ReadAllLines(plugin.Singleton.Config.ServerDir + @"\UserIdBans.txt");

            List<BannedUserInfo> BannedUserIds = new List<BannedUserInfo>();
            List<BannedUserInfo> BannedUserIps = new List<BannedUserInfo>();

            for (int i = 0; i < IpBansTxt.Length; i++)
            {
                string[] e = IpBansTxt[i].Split(';');
                BannedUserIps.Add(new BannedUserInfo(e[0], e[1]));
            }

            for (int i = 0; i < IdBansTxt.Length; i++)
            {
                string[] e = IdBansTxt[i].Split(';');
                BannedUserIds.Add(new BannedUserInfo(e[0], e[1]));
            }

            if (BannedUserIds.Count == 0)
            {
                Idbans = false;
            }
            else
            {
                for (int i = 0; i < BannedUserIds.Count; i++)
                {
                    plugin.IdBanlist.Add(new UserBans(BannedUserIds[i].Name, BannedUserIds[i].Ban, i));
                }
            }

            if (BannedUserIps.Count == 0)
            {
                Ipbans = false;
            }
            else
            {
                for (int i = 0; i < BannedUserIps.Count; i++)
                {
                    plugin.IpBanlist.Add(new UserBans(BannedUserIps[i].Name, BannedUserIps[i].Ban, i));
                }
            }

            if (Idbans == false && Ipbans == false)
            {
                plugin.IdBanlist = null;
                plugin.IpBanlist = null;
            }
            response = plugin.Singleton.Config.ReloadBansCmdDoneMsg;
            return true;
        }
    }
}
