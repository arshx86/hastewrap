#region

using System.IO;
using System.Linq;
using System.Text;
using Leaf.xNet;
using HttpResponse = Leaf.xNet.HttpResponse;
using StringContent = Leaf.xNet.StringContent;

#endregion

namespace HasteWRAP
{
    public class HasteBIN
    {
        private readonly string Proxy;
        private readonly string UserAgent = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.87 Safari/537.36 RuxitSynthetic/1.0 v4354928233897109899 t4157550440124640339";

        /// <summary>
        ///     Initializes a new hastebin wrapper.
        /// </summary>
        public HasteBIN(string proxy = null)
        {
            Proxy = proxy;
        }

        /// <summary>
        ///     Reads whole content from file, creates a haste.
        /// </summary>
        /// <param name="FilePath">Path of file to read.</param>
        /// <returns>Final result link of haste.</returns>
        public string CreateFromFile(string FilePath)
        {
            string Content = File.ReadAllText(FilePath);
            return Create(Content);
        }

        /// <summary>
        ///     Reads a text from hastebin link. Useful for directly data reading.
        /// </summary>
        /// <param name="Code">A full raw link or code.</param>
        /// <returns>Readed data from hastebin.</returns>
        public string Get(string Code)
        {
            const string Base_ = "https://hastebin.com/raw/";
            if (Code.Contains("toptal.com")) Code = Code.Split('/').Last();
            string Base = Base_ + Code;

            // Initialize clients.
            HttpRequest request = new HttpRequest
            {
                UserAgent = UserAgent
            };

            // If user preferred to use proxy, create parser.
            if (Proxy != null) request.Proxy = HttpProxyClient.Parse(Proxy);

            // Perform actions
            HttpResponse Act = request.Get(Base);
            return Act.ToString();
        }

        /// <summary>
        ///     Posts a plain text to hastebin.
        /// </summary>
        /// <param name="Content">Content to upload.</param>
        /// <returns>URL of the final hastebin result.</returns>
        public string Create(string Content)
        {
            const string api_endpoint = "https://hastebin.com/documents";
            byte[] UploadBytes = Encoding.UTF8.GetBytes(Content);

            HttpRequest req = new Leaf.xNet.HttpRequest
            {
                UserAgent = UserAgent
            };
            req.AddHeader("Content-Length", UploadBytes.Length.ToString());
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            if (Proxy != null) req.Proxy = HttpProxyClient.Parse(Proxy);

            /* Post */
            HttpResponse act = req.Post(api_endpoint, new StringContent(Content));
            string jsonjunk = act.ToString();

            /* Delete JSON Junks, Get Key */
            string GetCode = jsonjunk.Replace("{\"key\":\"", string.Empty).Replace("\"}", string.Empty);
            return $"https://www.toptal.com/developers/hastebin/{GetCode}";
        }
    }
}