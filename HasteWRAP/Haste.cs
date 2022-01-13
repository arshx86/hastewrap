using Leaf.xNet;
using System.Linq;
using System.Text;

namespace HasteWRAP
{

    public class Config
    {
        public string Proxy { get; set; }
        public string UserAgent { get; set; }
    }

    public class HasteBIN
    {

        private readonly bool UseProxy = false;
        private readonly string ProxyLink;
        private readonly string UserAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.87 Safari/537.36 RuxitSynthetic/1.0 v4354928233897109899 t4157550440124640339";

        /// <summary>
        ///  Initializes a new hastebin wrapper.
        /// </summary>
        /// <param name="ClientConfig">Custom config for client, contains advanced properties. Leave as null for use default config.</param>
        public HasteBIN(Config ClientConfig = null)
        {

            if (ClientConfig != null)
            {

                if (string.IsNullOrEmpty(ClientConfig.Proxy))
                {
                    ProxyLink = ClientConfig.Proxy;
                    UseProxy = true;
                }
                if (string.IsNullOrEmpty(ClientConfig.UserAgent))
                {
                    UserAgent = ClientConfig.UserAgent;
                }

            }


        }

        /// <summary>
        ///  Reads whole content from file, creates a haste.
        /// </summary>
        /// <param name="FilePath">Path of file to read.</param>
        /// <returns>Final result link of haste.</returns>
        public string CreateHasteFromFile(string FilePath)
        {

            if (!FilePath.EndsWith(".txt"))
            {
                FilePath = $"{FilePath}.txt";
            }

            string Content = System.IO.File.ReadAllText(FilePath);
            return CreateHaste(Content);

        }

        /// <summary>
        ///  Reads a text from hastebin link. Useful for directly data reading.
        /// </summary>
        /// <param name="Code">A full raw link or code.</param>
        /// <returns>Readed data from hastebin.</returns>
        public string GetHaste(string Code)
        {

            const string Base_ = "https://www.toptal.com/developers/hastebin/raw/";
            if (Code.Contains("toptal.com")) { Code = Code.Split('/').Last(); }
            string Base = Base_ + Code;

            // Initialize clients.
            HttpRequest request = new HttpRequest
            {
                UserAgent = UserAgent
            };

            // If user preferred to use proxy, create parser.
            if (UseProxy)
            {
                request.Proxy = HttpProxyClient.Parse(ProxyLink);
            }

            // Perform actions
            HttpResponse Act = request.Get(Base);
            return Act.ToString();

        }

        /// <summary>
        ///  Posts a plain text to hastebin.
        /// </summary>
        /// <param name="Content">Content to upload.</param>
        /// <returns>URL of the final hastebin result.</returns>
        public string CreateHaste(string Content)
        {

            const string api_endpoint = "https://www.toptal.com/developers/hastebin/documents";
            byte[] UploadBytes = Encoding.UTF8.GetBytes(Content);

            HttpRequest req = new HttpRequest
            {

                // Add headers.
                UserAgent = UserAgent
            };
            req.AddHeader("Content-Length", UploadBytes.Length.ToString());
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            // If user preferred to use proxy, create parser.
            if (UseProxy)
            {
                req.Proxy = HttpProxyClient.Parse(ProxyLink);
            }

            /* Perform action */

            HttpResponse act = req.Post(api_endpoint, new StringContent(Content));
            string jsonjunk = act.ToString();

            /* Delete JSON Junks, Get Key */
            string GetCode = jsonjunk.Replace("{\"key\":\"", string.Empty).Replace("\"}", string.Empty);
            return $"https://www.toptal.com/developers/hastebin/{GetCode}";

        }


    }
}
