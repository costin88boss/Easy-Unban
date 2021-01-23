namespace EasyUnban.Commands
{
    using CommandSystem;
    using System;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ListBans : ICommand
    {
        public string[] Aliases { get; } = EasyUnban.Singleton.Config.ListBansCmdAliases;

        public string Description { get; } = EasyUnban.Singleton.Config.ListBansCmdInfo;

        public string Command { get; } = EasyUnban.Singleton.Config.ListBansCmd;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = EasyUnban.Singleton.Config.ListBansCmdShowingListMsg;
            if (EasyUnban.IdBans != null)
            {
                response += "\n" + EasyUnban.Singleton.Config.ListBansCmdShowingIdList.Replace("{BanType}", "UserId");
                for (int i = 0; i < EasyUnban.IdBans.Count; i++)
                {
                    string message = EasyUnban.Singleton.Config.ListBansCmdBanList
                        .Replace("{BanNumber}", EasyUnban.IdBans[i].Id.ToString())
                        .Replace("{PlayerName}", EasyUnban.IdBans[i].Name);
                    response += "\n" + message;
                }
            }
            else response += "\n" + EasyUnban.Singleton.Config.NoIpOrIdBannedUserFound.Replace("{BanType}", "UserId");

            if (EasyUnban.IpBans != null)
            {
                response += "\n" + EasyUnban.Singleton.Config.ListBansCmdShowingIdList.Replace("{BanType}", "Ip");
                for (int i = 0; i < EasyUnban.IpBans.Count; i++)
                {
                    string message = EasyUnban.Singleton.Config.ListBansCmdBanList
                        .Replace("{BanNumber}", EasyUnban.IpBans[i].Id.ToString())
                        .Replace("{PlayerName}", EasyUnban.IpBans[i].Name);
                    response += "\n" + message;
                }
            }
            else response += "\n" + EasyUnban.Singleton.Config.NoIpOrIdBannedUserFound.Replace("{BanType}", "Ip");

            return true;
        }
    }
}