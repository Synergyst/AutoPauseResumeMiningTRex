using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace AutoPauseResumeMining {
    class Program {
        static void Main(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("ERROR, no password was specified!\n\tUsage: AutoPauseResumeMining.exe <webui_password_here>\n\nExiting..");
                Environment.Exit(0);
            }
            String pass = args[0];
            WebClient webClient = new WebClient();
            var json = webClient.DownloadString("http://127.0.0.1:4067/login?password=" + pass);
            dynamic data = JObject.Parse(json);
            if (data.success == "1") {
                Console.WriteLine("Logged into webUI API successfully!\nAttempting to pause the miner now.. please resume later manually via the webUI.\nAutomatic resume is not currently implemented.");
                var jsonPause = webClient.DownloadString("http://127.0.0.1:4067/control?sid=" + data.sid + "&pause=true");
                dynamic dataPause = JObject.Parse(jsonPause);
            } else {
                Console.WriteLine("ERROR: Could not login to webUI API..\nPlease ensure you have an API key set in your batch/JSON config first.\n\nExiting..");
            }
        }
    }
}
