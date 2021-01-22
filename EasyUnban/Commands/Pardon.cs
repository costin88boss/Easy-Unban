namespace EasyUnban.Commands
{
    using CommandSystem;
    using System;
    using System.Collections.Generic;
    using System.IO;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Pardon : ICommand
    {
        public string[] Aliases { get; } = EasyUnban.Singleton.Config.PardonCmdAliases;

        public string Description { get; } = EasyUnban.Singleton.Config.PardonCmdInfo;

        public string Command { get; } = EasyUnban.Singleton.Config.PardonCmd;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            BanHandler.GetPath(BanHandler.BanType.IP);
            if (arguments.Count != 0)
            {
                response = EasyUnban.Singleton.Config.PardonCmdRunningMsg;

                string plyName = arguments.At(0);

                string steamId = string.Empty;
                string userIp = string.Empty;

                string[] ipBansTxt = File.ReadAllLines(BanHandler.GetPath(BanHandler.BanType.IP));
                string[] idBansTxt = File.ReadAllLines(BanHandler.GetPath(BanHandler.BanType.UserId));

                List<BannedUserInfo> bannedUserIds = new List<BannedUserInfo>();
                List<BannedUserInfo> bannedUserIps = new List<BannedUserInfo>();

                for (int i = 0; i < ipBansTxt.Length; i++)
                {
                    string[] e = ipBansTxt[i].Split(';');
                    bannedUserIps.Add(new BannedUserInfo(e[0], e[1]));
                }

                for (int i = 0; i < idBansTxt.Length; i++)
                {
                    string[] e = idBansTxt[i].Split(';');
                    bannedUserIds.Add(new BannedUserInfo(e[0], e[1]));
                }

                if (bannedUserIps.Exists(e => e.Name == plyName))
                {
                    userIp = bannedUserIps.Find(e => e.Name == plyName).Ban;
                }
                else
                    response += "\n" +
                                EasyUnban.Singleton.Config.PardonCmdUserNotIpOrIdBanned.Replace("{BanType}", "Ip");

                if (bannedUserIds.Exists(e => e.Name == plyName))
                {
                    steamId = bannedUserIds.Find(e => e.Name == plyName).Ban;
                }
                else
                    response += "\n" +
                                EasyUnban.Singleton.Config.PardonCmdUserNotIpOrIdBanned.Replace("{BanType}", "UserId");

                if (steamId != string.Empty)
                    BanHandler.RemoveBan(steamId, BanHandler.BanType.UserId);
                if (userIp != string.Empty)
                    BanHandler.RemoveBan(userIp, BanHandler.BanType.IP);
                if (steamId == string.Empty && userIp == string.Empty)
                {
                    response += EasyUnban.Singleton.Config.PardonCmdUserIsNotBanned;
                    return false;
                }

                response += EasyUnban.Singleton.Config.PardonCmdUserIsUnbanned;
                return true;
            }

            response = EasyUnban.Singleton.Config.PardonCmdUserArgumentMissing;
            return false;
        }
    }
}