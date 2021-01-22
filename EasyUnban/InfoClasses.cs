namespace EasyUnban
{
    public class UserBans
    {
        public UserBans(string name, string ban, int id)
        {
            Name = name;
            Ban = ban;
            Id = id;
        }

        public string Name { get; }
        public string Ban { get; }
        public int Id { get; }
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