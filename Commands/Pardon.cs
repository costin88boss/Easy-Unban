using CommandSystem;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;

namespace Easy_Unban
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class Pardonare : ICommand
    {
        public string[] Aliases { get; } = plugin.Singleton.Config.PardonCmdAliases;

        public string Description { get; } = plugin.Singleton.Config.PardonCmdInfo;

        string ICommand.Command { get; } = plugin.Singleton.Config.PardonCmd;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            BanHandler.GetPath(BanHandler.BanType.IP);
            if (arguments.Count != 0)
            {
                response = plugin.Singleton.Config.PardonCmdRunningMsg;

                string plyName = arguments.At(0);

                string STEAMID = "";
                string USERIP = "";

                string[] IpBansTxt = File.ReadAllLines(BanHandler.GetPath(BanHandler.BanType.IP));
                string[] IdBansTxt = File.ReadAllLines(BanHandler.GetPath(BanHandler.BanType.UserId));

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

                if (BannedUserIps.Exists(e => e.Name == plyName))
                {
                    USERIP = BannedUserIps.Find(e => e.Name == plyName).Ban;
                }
                else response += "\n" + plugin.Singleton.Config.PardonCmdUserNotIpORIdBanned.Replace("{BanType}", "Ip");

                if (BannedUserIds.Exists(e => e.Name == plyName))
                {
                    STEAMID = BannedUserIds.Find(e => e.Name == plyName).Ban;
                }
                else response += "\n" + plugin.Singleton.Config.PardonCmdUserNotIpORIdBanned.Replace("{BanType}", "UserId");

                if (STEAMID != "")
                    BanHandler.RemoveBan(STEAMID, BanHandler.BanType.UserId);
                if (USERIP != "")
                    BanHandler.RemoveBan(USERIP, BanHandler.BanType.IP);
                if (STEAMID == "" && USERIP == "")
                {
                    response += plugin.Singleton.Config.PardonCmdUserIsNotBanned;
                    return false;
                }
                else
                {
                    response += plugin.Singleton.Config.PardonCmdUserIsUnbanned;
                    return true;
                }
            }
            else
            {
                response = plugin.Singleton.Config.PardonCmdUserArgumentMissing;
                return false;
            }
        }
    }
}
