namespace EasyUnban.Commands
{
    using CommandSystem;
    using System;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class PardonNum : ICommand
    {
        public string Command { get; } = EasyUnban.Singleton.Config.PardonNumCmd;

        public string[] Aliases { get; } = EasyUnban.Singleton.Config.PardonNumCmdAliases;

        public string Description { get; } = EasyUnban.Singleton.Config.PardonNumCmdInfo;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count >= 2)
            {
                response = EasyUnban.Singleton.Config.PardonNumCmdRunningMsg;

                if (!int.TryParse(arguments.At(1), out int id))
                {
                    response = "\n" + EasyUnban.Singleton.Config.PardonNumCmdNumArgInvalid;
                    return false;
                }

                if (arguments.At(0).ToLower() == "id")
                {
                    if (EasyUnban.IdBans.Exists(e => e.Id == id))
                    {
                        BanHandler.RemoveBan(EasyUnban.IdBans.Find(e => e.Id == id).Ban, BanHandler.BanType.UserId);
                    }
                    else
                    {
                        response += "\n" +
                                    EasyUnban.Singleton.Config.PardonNumCmdCantFindIpOrIdUserBannedWithNumber.Replace(
                                        "{BanType}", "UserId");
                    }
                }
                else if (arguments.At(0).ToLower() == "ip")
                {
                    if (EasyUnban.IpBans.Exists(e => e.Id == id))
                    {
                        BanHandler.RemoveBan(EasyUnban.IpBans.Find(e => e.Id == id).Ban, BanHandler.BanType.IP);
                    }
                    else
                    {
                        response += "\n" +
                                    EasyUnban.Singleton.Config.PardonNumCmdCantFindIpOrIdUserBannedWithNumber.Replace(
                                        "{BanType}", "Ip");
                    }
                }
                else
                {
                    response += "\n" + EasyUnban.Singleton.Config.PardonNumFirstArgBeIdOrIp;
                    return false;
                }

                return true;
            }

            response = "\n" + EasyUnban.Singleton.Config.PardonNumArgumentsMissing;
            return false;
        }
    }
}