namespace EasyUnban
{
    using Exiled.API.Features;
    using System.Collections.Generic;
    using System.IO;

    public class EasyUnban : Plugin<Config>
    {
        public static EasyUnban Singleton;

        public static List<UserBans> IpBans = new List<UserBans>();
        public static List<UserBans> IdBans = new List<UserBans>();

        public override void OnEnabled()
        {
            Singleton = this;
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
                    IdBans.Add(new UserBans(bannedUserIds[i].Name, bannedUserIds[i].Ban, i));
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
                    IpBans.Add(new UserBans(bannedUserIps[i].Name, bannedUserIps[i].Ban, i));
                }
            }

            if (idBans == false && ipBans == false)
            {
                IdBans = null;
                IpBans = null;
            }

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Singleton = null;
            base.OnDisabled();
        }
    }
}