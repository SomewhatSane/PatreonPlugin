using System;
using System.Text;
using Exiled.API.Features;
using PatreonPlugin.Handlers;
using Utf8Json;
using System.IO;
using PlayerEvents = Exiled.Events.Handlers.Player;


namespace PatreonPlugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "PatreonPlugin";
        public override string Author { get; } = "Kat";
        public override string Prefix { get; } = "pp";
        public override Version RequiredExiledVersion { get; } = new Version("2.1.23");

        internal const string version = "1.1.1";
        internal const string LastModified = "2021/01/06 22:19 UTC";

        public static PatreonConfig PatreonConfig;
        private UpdateChecker UpdateChecker;
        private PlayerEventHandlers PlayerEventHandlers;

        private static readonly string ConfigurationFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EXILED/Plugins/PatreonPlugin/");
        public static readonly string ReservedSlotsFilePath = GameCore.ConfigSharing.Paths[3] + "UserIDReservedSlots.txt";


        public override void OnEnabled()
        {
            if (!Config.IsEnabled) return;

            Log.Info($"{Name} v{version} by {Author}. Last Modified: {LastModified}.");
            Log.Info("Originally a port of DankRushen/PatreonPlugin on GitHub.");

            Log.Info("Loading base scripts.");
            UpdateChecker = new UpdateChecker(this);

            if (Config.CheckForUpdates)
                _ = UpdateChecker.CheckForUpdate();

            try
            {
                Log.Info("Loading patreon configuration.");
                PatreonConfig = new PatreonConfig();

                if (!Directory.Exists(ConfigurationFolderPath))
                {
                    Log.Info($"{ConfigurationFolderPath} does not exist. Creating folder...");
                    Directory.CreateDirectory(ConfigurationFolderPath);
                }


                if (File.Exists(ConfigurationFolderPath + "Configuration.json"))
                    PatreonConfig = LoadPatreonConfig();
                else
                {
                    Log.Info($"{ConfigurationFolderPath}/Configuration.json does not exist. Saving the defaults...");
                    Log.Info("Change the configuration to suit your needs!!");
                    SavePatreonConfig();
                }
                    
            }
            
            catch(Exception ex)
            {
                Log.Error($"Patreon Configuration parse error. Exception: {ex}");
            }

            Log.Info("Registering Event Handlers.");

            PlayerEventHandlers = new PlayerEventHandlers(this);
            PlayerEvents.Joined += PlayerEventHandlers.Joined;
            PlayerEvents.Spawning += PlayerEventHandlers.Spawning;


            Log.Info("Done.");
        }

        public override void OnDisabled()
        {
            if (!Config.IsEnabled) return;

            PlayerEvents.Joined -= PlayerEventHandlers.Joined;
            PlayerEvents.Spawning -= PlayerEventHandlers.Spawning;
            PlayerEventHandlers = null;

            PatreonConfig = null;

            Log.Info("Disabled.");
        }

        public static PatreonConfig LoadPatreonConfig()
        {
            return JsonSerializer.Deserialize<PatreonConfig>(File.ReadAllText(ConfigurationFolderPath + "Configuration.json"));
        }

        public static void SavePatreonConfig()
        {
            File.WriteAllText(ConfigurationFolderPath + "Configuration.json", Encoding.UTF8.GetString(JsonSerializer.Serialize(PatreonConfig))); //Save default if the config file doesn't exist.
        }
    }
}
