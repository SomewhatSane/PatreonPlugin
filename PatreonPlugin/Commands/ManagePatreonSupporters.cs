using System;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using CommandSystem;
using RemoteAdmin;


namespace PatreonPlugin.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class ManagePatreonSupporters : ICommand
    {
        public string Command { get; } = "patreon";
        public string[] Aliases { get; } = { "pi" };

        public string Description { get; } = "Manage Patreon supporters.";

        public bool Execute(ArraySegment<string> Arguments, ICommandSender Sender, out string Response)
        {
            string UserId;
            string RankName;

            if (Sender is PlayerCommandSender Player)
            {
                if (!Permissions.CheckPermission(Player, "pp.manage"))
                {
                    Response = $@"You are lacking the permission 'pp.manage' that is required to run this command.";
                    return false;
                }
            }
            

            if (Arguments.Array.Length < 2)
            {
                Response = "You need to pass some parameters. Valid parameters are: add, delete & reload.";
                return false;
            }

            switch (Arguments.Array[1].ToUpper())
            {
                case "ADD":

                    //Check to make sure all parameters are there.

                    if (Arguments.Array.Length != 4)
                    {
                        Response = "Some required parameters are missing. To add a patreon rank, use PATREON ADD SomeUserIDHere@steam SomeRankNameHere";
                        return false;
                    }
                    
                    //Try to parse user id.
                    try
                    {
                        if (Arguments.Array[2].Contains("@"))
                            UserId = Arguments.Array[2];

                        else
                            UserId = Exiled.API.Features.Player.Get(Arguments.Array[2]).UserId;
                    }

                    catch(Exception ex)
                    {
                        Response = $"An exception when trying to parse argument 3 (UserId). Exception: {ex}";
                        return false;
                    }

                    RankName = Arguments.Array[3];

                    //Check to see if they already have a rank. If they do, then tell them that.

                    if (Plugin.PatreonConfig.Patreons.ContainsKey(UserId))
                    {
                        Response = $"{Arguments.Array[2]} already has a patreon rank {Plugin.PatreonConfig.Patreons[Arguments.Array[3].ToLower()]}";
                        return false;
                    }

                    //Check to see if the rank that they want to add actually exists.

                    if (Plugin.PatreonConfig.PatreonRanks[RankName] == null)
                    {
                        Response = $"The rank {RankName} does not exist.";
                        return false;
                    }

                    //All tests pass. Add the role and reserialise.

                    Plugin.PatreonConfig.Patreons.Add(UserId, RankName);

                    Log.Info("A patreon has been added. Saving configuration...");
                    Plugin.SavePatreonConfig();

                    Response = $"Given {UserId} the patreon rank {RankName}.";
                    return true;

                case "REMOVE":
                case "DELETE":

                    if (Arguments.Array.Length != 3)
                    {
                        Response = "Some required parameters are missing. To delete / remove a patreon rank, use PATREON DELETE SomeUserIDHere@steam";
                        return false;
                    }

                    //Try to parse user id.
                    try
                    {
                        if (Arguments.Array[2].Contains("@"))
                            UserId = Arguments.Array[2];

                        else
                            UserId = Exiled.API.Features.Player.Get(Arguments.Array[2]).UserId;
                    }

                    catch (Exception ex)
                    {
                        Response = $"An exception when trying to parse argument 3 (UserId). Exception: {ex}";
                        return false;
                    }

                    //Check to make sure they actually have a rank that can be removed.
                    if (!Plugin.PatreonConfig.Patreons.ContainsKey(Arguments.Array[2].ToLower()))
                    {
                        Response = $"{Arguments.Array[2]} does not have a patreon rank to remove.";
                        return false;
                    }

                    //All tests pass. Remove the role and reserialise.

                    Plugin.PatreonConfig.Patreons.Remove(Arguments.Array[2].ToLower());

                    Log.Info("A patreon has been deleted. Saving and reloading configuration.");
                    Plugin.SavePatreonConfig();
                    Plugin.LoadPatreonConfig();

                    Response = $"Removed patreon rank from {UserId}. If they have a reserved slot, it needs to be removed manually.";
                    return true;

                case "RELOAD":
                case "R":

                    //We don't have to check for parameters here because the command doesn't need any :)
                    
                    try
                    {
                        Log.Info("Reloading PatreonPlugin configuration.");
                        Plugin.PatreonConfig = Plugin.LoadPatreonConfig();
                        Response = "Reloaded PatreonPlugin configuration.";
                        return true;
                    }

                    catch (Exception ex)
                    {
                        Response = $"There was an error when trying to reload configuration. Exception: {ex}";
                        return false;
                    }

                default:
                    Response = $"Unknown parameter '{Arguments.Array[1]}'. Valid parameters are: add, delete & reload.";
                    return false;
            }
        }
    }
}
