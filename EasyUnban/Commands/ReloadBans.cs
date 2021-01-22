namespace EasyUnban.Commands
{
    using CommandSystem;
    using System;
    using System.Collections.Generic;
    using System.IO;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ReloadBans : ICommand
    {
        public string Command { get; } = EasyUnban.Singleton.Config.ReloadBansCmd;

        public string[] Aliases { get; } = EasyUnban.Singleton.Config.ReloadBansCmdAliases;

        public string Description { get; } = EasyUnban.Singleton.Config.ReloadBansCmdInfo;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            EasyUnban.IpBans = new List<UserBans>();
            EasyUnban.IdBans = new List<UserBans>();

            bool ipBans = true;
            bool idBans = true;

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

            if (bannedUserIds.Count == 0)
            {
                idBans = false;
            }
            else
            {
                for (int i = 0; i < bannedUserIds.Count; i++)
                {
                    EasyUnban.IdBans.Add(new UserBans(bannedUserIds[i].Name, bannedUserIds[i].Ban, i));
                }
            }

            if (bannedUserIps.Count == 0)
            {
                ipBans = false;
            }
            else
            {
                for (int i = 0; i < bannedUserIps.Count; i++)
                {
                    EasyUnban.IpBans.Add(new UserBans(bannedUserIps[i].Name, bannedUserIps[i].Ban, i));
                }
            }

            if (idBans == false && ipBans == false)
            {
                EasyUnban.IdBans = null;
                EasyUnban.IpBans = null;
            }

            response = EasyUnban.Singleton.Config.ReloadBansCmdDoneMsg;
            return true;
        }
    }
}