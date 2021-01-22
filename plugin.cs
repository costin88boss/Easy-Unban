using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.IO;

namespace Easy_Unban
{
    public class plugin : Plugin<Config>
    {
        public static plugin Singleton;
        public override PluginPriority Priority { get; } = PluginPriority.Medium;
        
        public static List<UserBans> IpBanlist = new List<UserBans>();
        public static List<UserBans> IdBanlist = new List<UserBans>();

        public override void OnEnabled()
        {
            bool Ipbans = true;
            bool Idbans = true;

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

            if (BannedUserIds.Count == 0)
            {
                Idbans = false;
            }
            else
            {
                for (int i = 0; i < BannedUserIds.Count; i++)
                {
                    IdBanlist.Add(new UserBans(BannedUserIds[i].Name, BannedUserIds[i].Ban, i));
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
                    IpBanlist.Add(new UserBans(BannedUserIps[i].Name, BannedUserIps[i].Ban, i));
                }
            }

            if (Idbans == false && Ipbans == false)
            {
                IdBanlist = null;
                IpBanlist = null;
            }
        }
    }


    public class UserBans
    {
        public UserBans(string name, string ban, int id)
        {
            Name = name;
            Ban = ban;
            Id = id;
        }
        public string Name;
        public string Ban;
        public int Id;
    }

    public class BannedUserInfo
    {
        public BannedUserInfo(string name, string ban)
        {
            Name = name;
            Ban = ban;
        }
        public string Name { get; }
        public string Ban { get; }

    }
}
