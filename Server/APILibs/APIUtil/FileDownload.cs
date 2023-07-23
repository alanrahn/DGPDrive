using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiUtil
{
    public class DGPFileDownload
    {

        // http://www.tugberkugurlu.com/archive/efficiently-streaming-large-http-responses-with-httpclient
        static async Task HttpGetForLargeFileInRightWay()
        {
            using (HttpClient client = new HttpClient())
            {
                const string url = "https://github.com/tugberkugurlu/ASPNETWebAPISamples/archive/master.zip";
                using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                {
                    string fileToWriteTo = Path.GetTempFileName();
                    using (Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
                    {
                        await streamToReadFrom.CopyToAsync(streamToWriteTo);
                    }
                }
            }
        }
    }
}
