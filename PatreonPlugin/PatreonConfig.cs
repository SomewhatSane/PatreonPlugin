using System.Collections.Generic;

namespace PatreonPlugin
{
    public class PatreonConfig
    {
        public Dictionary<string, PatreonRank> PatreonRanks { get; set; } = new Dictionary<string, PatreonRank>()
        {
            {"None", new PatreonRank{Tag = "NOTUSED", TagColour="NOTUSED" } }, //Used for items only to allow servers not to be P2W.
            {"Default", new PatreonRank{Tag = "Default Tag", TagColour = "pumpkin", AutoReserve = false} },
            {"MegaDonator", new PatreonRank{Tag = "Mega Donator", TagColour = "red", AutoReserve = true} }
        };
        public Dictionary<string, Patreon> Patreons { get; set; } = new Dictionary<string, Patreon>()
        {
            {"SomeUserIdHere@steam", new Patreon{RankName = "Default", OverrideRATag = false } },
            {"AnotherUserIdHere@discord", new Patreon{RankName = "MegaDonator", OverrideRATag = false} }
        };
    }

    public class PatreonRank
    {
        public string Tag { get; set; }
        public string TagColour { get; set; }

        public Dictionary<string, List<string>> ExtraItems { get; set; } = new Dictionary<string, List<string>>()
        {
            {"ClassD", new List<string> {"Flashlight:1"} },
            {"Scientist", new List<string> {"Flashlight:1"} }
        };

        public bool AutoReserve { get; set; }
    }

    public class Patreon
    {
        public string RankName { get; set; }
        public bool OverrideRATag { get; set; }
    }
}
