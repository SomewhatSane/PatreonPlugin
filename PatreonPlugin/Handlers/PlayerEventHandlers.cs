using Exiled.Events.EventArgs;
using Exiled.API.Features;
using System.IO;
using MEC;

namespace PatreonPlugin.Handlers
{
    public class PlayerEventHandlers
    {
        private readonly Plugin plugin;
        public PlayerEventHandlers(Plugin plugin) => this.plugin = plugin;

        public void Joined(JoinedEventArgs ev)
        {
            //If they are a patreon.
            if (Plugin.PatreonConfig.Patreons.ContainsKey(ev.Player.UserId))
            {
                string RankName = Plugin.PatreonConfig.Patreons[ev.Player.UserId];
                
                //If the rank given actually exists.
                if (Plugin.PatreonConfig.PatreonRanks.ContainsKey(RankName))
                {
                    //Give ranks.
                    ev.Player.RankName = Plugin.PatreonConfig.PatreonRanks[RankName].Tag;
                    ev.Player.RankColor = Plugin.PatreonConfig.PatreonRanks[RankName].TagColour;

                    if (Plugin.PatreonConfig.PatreonRanks[RankName].AutoReserve && !ReservedSlot.HasReservedSlot(ev.Player.UserId))
                    {
                        Log.Info($"Adding reserved slot for {ev.Player.UserId} due to patreon rank auto reserve.");
                        using (StreamWriter ReservedSlotWriter = File.AppendText(Plugin.ReservedSlotsFilePath))
                        {
                            ReservedSlotWriter.WriteLine($"#{ev.Player.Nickname} ({ev.Player.UserId}) - Patreon Rank: {RankName}.");
                            ReservedSlotWriter.WriteLine(ev.Player.UserId);
                        }
                        ReservedSlot.Reload();
                    }

                    ev.Player.SendConsoleMessage("Thank you for being a server patreon.", "green");
                }

                else
                    Log.Warn($"{ev.Player.UserId} has been assigned a patreon rank that does not exist. Skipping.");


            }
        }

        public void Spawning(SpawningEventArgs ev)
        {
            if (ev.RoleType != RoleType.None || ev.RoleType != RoleType.Spectator)
            {
                //If they are a patreon.
                if (Plugin.PatreonConfig.Patreons.ContainsKey(ev.Player.UserId))
                {
                    string RankName = Plugin.PatreonConfig.Patreons[ev.Player.UserId];
                    //See if the rank actually exists.
                    if (Plugin.PatreonConfig.PatreonRanks.ContainsKey(RankName))
                       Timing.CallDelayed(1f, () => ItemManager.GiveItems(ev.Player, Plugin.PatreonConfig.PatreonRanks[RankName].ExtraItems[ItemManager.RoleToString(ev.Player.Role)]));

                    else
                        Log.Warn($"{ev.Player.UserId} has been assigned a patreon rank that does not exist. Skipping.");
                }
            }
        }
    }
}
