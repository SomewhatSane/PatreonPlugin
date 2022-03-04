using System;
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

        public void Verified(VerifiedEventArgs ev)
        {
            //If they are a patreon.
            if (Plugin.PatreonConfig.Patreons.ContainsKey(ev.Player.UserId))
            {
                string RankName = Plugin.PatreonConfig.Patreons[ev.Player.UserId].RankName;

                //If the rank given actually exists.
                if (Plugin.PatreonConfig.PatreonRanks.ContainsKey(RankName))
                {

                    if (string.IsNullOrWhiteSpace(ev.Player.RankName) || Plugin.PatreonConfig.Patreons[ev.Player.UserId].OverrideRATag)
                    {
                        //Needed to add this delay so that if the player should have a PatreonPlugin rank but also has a RA rank, the player will get the RA rank's tag first before we replace it with PatreonPlugin's.
                        Timing.CallDelayed(1f, () => SetRank(ev.Player, Plugin.PatreonConfig.PatreonRanks[RankName].Tag, Plugin.PatreonConfig.PatreonRanks[RankName].TagColour));
                    }

                    if (Plugin.PatreonConfig.PatreonRanks[RankName].AutoReserve && !ReservedSlot.HasReservedSlot(ev.Player.UserId))
                    {
                        Log.Info($"Adding reserved slot for {ev.Player.UserId} due to patreon rank auto reserve flag.");
                        using (StreamWriter ReservedSlotWriter = File.AppendText(Plugin.ReservedSlotsFilePath))
                        {
                            if (!File.ReadAllText(Plugin.ReservedSlotsFilePath).EndsWith("\n", StringComparison.Ordinal))
                                ReservedSlotWriter.Write("\n");
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
                    string RankName = Plugin.PatreonConfig.Patreons[ev.Player.UserId].RankName;
                    //See if the rank actually exists.
                    if (Plugin.PatreonConfig.PatreonRanks.ContainsKey(RankName))
                       Timing.CallDelayed(1f, () => ItemManager.GiveItems(ev.Player, Plugin.PatreonConfig.PatreonRanks[RankName].ExtraItems[ev.Player.Role.ToString()]));

                    else
                        Log.Warn($"{ev.Player.UserId} has been assigned a patreon rank that does not exist. Skipping.");
                }
                else //If they are not a patreon, give the items in the None rank.
                {
                    Timing.CallDelayed(1f, () => ItemManager.GiveItems(ev.Player, Plugin.PatreonConfig.PatreonRanks["None"].ExtraItems[ev.Player.Role.ToString()]));
                }
            }
        }

        private void SetRank(Player Player, string RankName, string RankColour)
        {
            Player.RankName = RankName;
            Player.RankColor = RankColour;
        }
    }
}
