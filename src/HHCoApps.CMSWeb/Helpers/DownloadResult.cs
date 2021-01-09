using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace HHCoApps.CMSWeb.Helpers
{
    public class DownloadResult : IHttpActionResult
    {
        byte[] bookStuff;
        string fileName;
        HttpRequestMessage httpRequestMessage;
        HttpResponseMessage httpResponseMessage;
        public DownloadResult(byte[] data, HttpRequestMessage request, string filename)
        {
            bookStuff = data;
            httpRequestMessage = request;
            fileName = filename;
        }
        public System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK);
            httpResponseMessage.Content = new ByteArrayContent(bookStuff);
            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = fileName };            
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            httpResponseMessage.Content.Headers.Add("x-filename", fileName);

            return System.Threading.Tasks.Task.FromResult(httpResponseMessage);
        }
    }
}