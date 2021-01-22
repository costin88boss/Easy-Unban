using CommandSystem;
using Easy_Unban;
using System;

namespace Easy_UnBan.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class PardonNum : ICommand
    {
        public string Command { get; } = plugin.Singleton.Config.PardonNumCmd;

        public string[] Aliases { get; } = plugin.Singleton.Config.PardonNumCmdAliases;

        public string Description { get; } = plugin.Singleton.Config.PardonNumCmdInfo;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            if (arguments.Count >= 2)
            {
                response = plugin.Singleton.Config.PardonNumCmdRunningMsg;
                try
                {
                    int.Parse(arguments.At(1));
                }
                catch
                {
                    response += "\n" + plugin.Singleton.Config.PardonNumCmdNumArgInvalid;
                    return false;
                }

                int ID = int.Parse(arguments.At(1));

                if (arguments.At(0).ToLower() == "id")
                {
                    if (plugin.IdBanlist.Exists(e => e.Id == ID))
                    {
                        BanHandler.RemoveBan(plugin.IdBanlist.Find(e => e.Id == ID).Ban, BanHandler.BanType.UserId);
                    }
                    else
                    {
                        response += "\n" + plugin.Singleton.Config.PardonNumCmdCantFindIpORIdUserBannedWithNumber.Replace("{BanType}", "UserId");
                    }
                }
                else if (arguments.At(0).ToLower() == "ip")
                {
                    if (plugin.IpBanlist.Exists(e => e.Id == ID))
                    {
                        BanHandler.RemoveBan(plugin.IpBanlist.Find(e => e.Id == ID).Ban, BanHandler.BanType.IP);
                    }
                    else
                    {
                        response += "\n" + plugin.Singleton.Config.PardonNumCmdCantFindIpORIdUserBannedWithNumber.Replace("{BanType}", "Ip");
                    }
                }
                else
                {
                    response += "\n" + plugin.Singleton.Config.PardonNumFirstArgBeIdOrIp;
                    return false;
                }

                return true;
            }
            else
            {
                response = "\n" + plugin.Singleton.Config.PardonNumArgumentsMissing;
                return false;
            }
        }
    }
}
