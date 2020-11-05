using System.Collections.Generic;
using System.Linq.Expressions;

namespace PatreonPlugin
{
    public class PatreonConfig
    {
        public Dictionary<string, PatreonRank> PatreonRanks { get; set; } = new Dictionary<string, PatreonRank>()
        {
            {"Default", new PatreonRank{Tag = "Default Tag", TagColour = "pumpkin", AutoReserve = false} },
            {"MegaDonator", new PatreonRank{Tag = "Mega Donator", TagColour = "red", AutoReserve = true} }
        };
        public Dictionary<string, string> Patreons { get; set; } = new Dictionary<string, string>()
        {
            {"SomeUserIdHere@steam", "Default" },
            {"AnotherUserIdHere@discord", "MegaDonator" }
        };
    }

    public class PatreonRank
    {
        public string Tag { get; set; }
        public string TagColour { get; set; }

        public Dictionary<string, List<string>> ExtraItems { get; set; } = new Dictionary<string, List<string>>()
        {
            {"ClassD", new List<string>() {"Flashlight"} },
            {"Scientist", new List<string>() {"Flashlight"} }
        };

        public bool AutoReserve { get; set; }
    }
}
