using Exiled.API.Features;
using Exiled.API.Interfaces;
using Microsoft.SqlServer.Server;
using System.ComponentModel;

namespace Easy_Unban
{
    public class Config : IConfig
    {
        [Description("Should the plugin be enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("The directory to the server configurations that includes the Banned users text files. replace \"PUT USER HERE\" with the user you are currently using your pc. Server port is automatically detected.")]
        public string ServerDir { get; set; } = @"C:\Users\[PUT USER HERE]\AppData\Roaming\SCP Secret Laboratory\config\" + Server.Port;


        [Description("---ListBans command--- (Command info)")]
        public string ListBansCmd { get; set; } = "ListBans";
        public string ListBansCmdInfo { get; set; } = "Shows a list of ID and IP bans. Use the number on the left of a user with \"PardonNum\" to unban him easier in case of stuff like Unicode.";
        public string[] ListBansCmdAliases { get; set; } = { "BanList", "ListBan", "Lb" };

        [Description("---ListBans command--- (Command messages)")]
        public string ListBansCmdShowingListMsg { get; set; } = "Showing list..";
        public string ListBansCmdShowingIdList { get; set; } = "---Id bans---";
        [Description("This is how the ListBans commands aligns the users numbers and their names. use {BanNumber} and {PlayerName}.")]
        public string ListBansCmdBanList { get; set; } = "{BanNumber} |-| {PlayerName}";
        [Description("use {BanType} as it's replaced with what ban type there are no users (Ip or Userid bans)")]
        public string NoIpORIdbannedUserFound { get; set; } = "No {BanType} banned users found.";


        [Description("---Pardon command--- (Command info)")]
        public string PardonCmd { get; set; } = "Pardon";
        public string PardonCmdInfo { get; set; } = "Pardon the user from both Ip and UserId Ban lists, by simply inputting his username. (won't work for Unicode users :( )";
        public string[] PardonCmdAliases { get; set; } = { "Prdn" };
        [Description("---Pardon command--- (Command messages)")]

        public string PardonCmdRunningMsg { get; set; } = "Pardoning user..";
        [Description("use {BanType} as it's replaced with what ban type the user is not banned")]
        public string PardonCmdUserNotIpORIdBanned { get; set; } = "The user is not {BanType} banned!";
        public string PardonCmdUserIsNotBanned { get; set; } = "did not pardon the user. he is not banned.";
        public string PardonCmdUserIsUnbanned { get; set; } = "successfully pardoned the user!";
        public string PardonCmdUserArgumentMissing { get; set; } = "please enter the user's name";


        [Description("---PardonNum command--- (Command Info)")]
        public string PardonNumCmd { get; set; } = "PardonNum";
        public string PardonNumCmdInfo { get; set; } = "Pardon the User using a simple number that  you get in the \"ListBans\" command. make sure you execute \"ReloadBans\" if user is missing but you know he is banned.";
        public string[] PardonNumCmdAliases { get; set; } = { "prdN", "prdnum", "PardonN" };
        [Description("---PardonNum command--- (Command messages)")]

        public string PardonNumCmdRunningMsg { get; set; } = "pardoning with number..";
        public string PardonNumCmdNumArgInvalid { get; set; } = "the second argument must be a number!";
        [Description("use {BanType} as it's replaced with what ban type the user is not banned")]
        public string PardonNumCmdCantFindIpORIdUserBannedWithNumber { get; set; } = "Could not find a {BanType} User with that number!";
        public string PardonNumFirstArgBeIdOrIp { get; set; } = "first argument must be either Id or Ip!";
        public string PardonNumArgumentsMissing { get; set; } = "arguments missing! first argument should be either ip or id (depending on how the user is banned), or try both. the second argument has to be the number you've got from \"ListBans\" command.";
        
        
        [Description("---ReloadBans command--- (Command info)")]
        public string ReloadBansCmd { get; set; } = "ReloadBans";
        public string ReloadBansCmdInfo { get; set; } = "Use this command if a banned user is missing from the \"ListBans\" command or it's empty.";
        public string[] ReloadBansCmdAliases { get; set; } = { "RldBns", "RldB", "RldBans", "RBans" };

        [Description("---ReloadBans command--- (Command messages)")]
        public string ReloadBansCmdDoneMsg { get; set; } = "Done! you can now try executing \"ListBans\" command again.";


    }
}
