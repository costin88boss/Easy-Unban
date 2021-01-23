namespace EasyUnban
{
    using Exiled.API.Interfaces;
    using System.ComponentModel;

    public sealed class Config : IConfig
    {
        [Description("Should the plugin be enabled?")]
        public bool IsEnabled { get; set; } = true;
        [Description("Set whetever or not the plugin will detect the ban list files or you can assign your location.")]
        public bool ManualDirectory { get; private set; } = false;
        public string ManualIpBanDirectory { get; private set; } = @"C:\Users\[yourname]\AppData\Roaming\SCP Secret Laboratory\config\7777\UserIdBans.txt [NOTE THIS IS AN EXAMPLE]";
        public string ManualIdBanDirectory { get; private set; } = @"C:\Users\[yourname]\AppData\Roaming\SCP Secret Laboratory\config\7777\IpBans.txt [NOTE THIS IS AN EXAMPLE]";

        [Description("---ListBans command--- (Command info)")]
        public string ListBansCmd { get; private set; } = "ListBans";

        public string ListBansCmdInfo { get; private set; } =
            "Shows a list of ID and IP bans. Use the number on the left of a user with \"PardonNum\" to unban him easier in case of stuff like Unicode.";

        public string[] ListBansCmdAliases { get; private set; } = {"BanList", "ListBan", "Lb"};

        [Description("---ListBans command--- (Command messages)")]
        public string ListBansCmdShowingListMsg { get; private set; } = "Showing list..";
        [Description("{BanType} will be replaced with the ban type for the 2 lists.")]
        public string ListBansCmdShowingIdList { get; private set; } = "---{BanType} bans---";

        [Description(
            "This is how the ListBans commands aligns the users numbers and their names. use {BanNumber} and {PlayerName}.")]
        public string ListBansCmdBanList { get; private set; } = "{BanNumber} |-| {PlayerName}";

        [Description("use {BanType} as it's replaced with what ban type there are no users (Ip or Userid bans)")]
        public string NoIpOrIdBannedUserFound { get; private set; } = "No {BanType} banned users found.";


        [Description("---Pardon command--- (Command info)")]
        public string PardonCmd { get; private set; } = "Pardon";

        public string PardonCmdInfo { get; private set; } =
            "Pardon the user from both Ip and UserId Ban lists, by simply inputting his username. (won't work for Unicode users :( )";

        public string[] PardonCmdAliases { get; private set; } = { "Prdn" };

        [Description("---Pardon command--- (Command messages)")]

        public string PardonCmdRunningMsg { get; private set; } = "Pardoning user..";

        [Description("use {BanType} as it's replaced with what ban type the user is not banned")]
        public string PardonCmdUserNotIpOrIdBanned { get; private set; } = "The user is not {BanType} banned!";

        public string PardonCmdUserIsNotBanned { get; private set; } = "Did not pardon the user, they are not banned.";
        public string PardonCmdUserIsUnbanned { get; private set; } = "Successfully pardoned the user!";
        public string PardonCmdUserArgumentMissing { get; private set; } = "Please enter the user's name";


        [Description("---PardonNum command--- (Command Info)")]
        public string PardonNumCmd { get; private set; } = "PardonNum";

        public string PardonNumCmdInfo { get; private set; } =
            "Pardon the User using a simple number that you get in the \"ListBans\" command. make sure you execute \"ReloadBans\" if user is missing but you know he is banned.";

        public string[] PardonNumCmdAliases { get; private set; } = {"prdnum", "PardonN"};

        [Description("---PardonNum command--- (Command messages)")]

        public string PardonNumCmdRunningMsg { get; private set; } = "pardoning with number..";

        public string PardonNumCmdNumArgInvalid { get; private set; } = "the second argument must be a number!";

        [Description("use {BanType} as it's replaced with what ban type the user is not banned")]
        public string PardonNumCmdCantFindIpOrIdUserBannedWithNumber { get; private set; } =
            "Could not find a {BanType} User with that number!";

        public string PardonNumFirstArgBeIdOrIp { get; private set; } = "first argument must be either Id or Ip!";

        public string PardonNumArgumentsMissing { get; private set; } =
            "Arguments missing! first argument should be either ip or id (depending on how the user is banned), or try both. the second argument has to be the number you've got from \"ListBans\" command.";


        [Description("---ReloadBans command--- (Command info)")]
        public string ReloadBansCmd { get; private set; } = "ReloadBans";

        public string ReloadBansCmdInfo { get; private set; } =
            "Use this command if a banned user is missing from the \"ListBans\" command or it's empty.";

        public string[] ReloadBansCmdAliases { get; private set; } = {"RldBns", "RldB", "RldBans", "RBans"};

        [Description("---ReloadBans command--- (Command messages)")]
        public string ReloadBansCmdDoneMsg { get; private set; } =
            "Done! you can now try executing \"ListBans\" command again.";
    }
}