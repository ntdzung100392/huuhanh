using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HHCoApps.CMSWeb.Helpers
{
    public class DownloadResult : IHttpActionResult
    {
        byte[] bookStuff;
        string FileName;
        HttpRequestMessage httpRequestMessage;
        HttpResponseMessage httpResponseMessage;
        public DownloadResult(byte[] data, HttpRequestMessage request, string filename)
        {
            bookStuff = data;
            httpRequestMessage = request;
            FileName = filename;
        }
        public System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK);
            httpResponseMessage.Content = new ByteArrayContent(bookStuff);
            httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = FileName;
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");

            return System.Threading.Tasks.Task.FromResult(httpResponseMessage);
        }
    }
}