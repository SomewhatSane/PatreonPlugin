using System.Net.Http;
using System.Threading.Tasks;
using Exiled.API.Features;

namespace PatreonPlugin
{
    public class UpdateChecker
    {
        private readonly Plugin plugin;
        public UpdateChecker(Plugin plugin) => this.plugin = plugin;

        public async Task CheckForUpdate()
        {
            Log.Info("Checking for update.");
            using (HttpClient Client = new HttpClient())
            {
                Client.DefaultRequestHeaders.Add("User-Agent", $"{plugin.Name} Update Checker - Running v{plugin.Version}");
                HttpResponseMessage ResponseMessage = await Client.GetAsync("https://scpsl.somewhatsane.co.uk/plugins/patreonplugin/latest.html");
                if (!ResponseMessage.IsSuccessStatusCode)
                {
                    Log.Error($"There was an error when checking for update. Please try again later. Error: {(int)ResponseMessage.StatusCode} {ResponseMessage.StatusCode}");
                    return;
                }

                string[] Response = ResponseMessage.Content.ReadAsStringAsync().Result.Split('/');

                if (Response[0] == Plugin.version)
                    Log.Info($"You are running the latest version of {plugin.Name}");
                else
                {
                    Log.Info($"There is an update to {plugin.Name} available. Download it from: {Response[1]} .");

                    if (Response[2] != null)
                        Log.Info($"Note from plugin author: {Response[2]}");
                }
            }
        }
    }
}
