using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using OfficeOpenXml;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace HHCoApps.CMSWeb.Controllers
{
    public class ReportsController : UmbracoApiController
    {
        private int _rowNumber = 1;

        [HttpGet]
        public HttpResponseMessage DownloadSiteContent([FromUri]string username, [FromUri]string password)
        {
            var signInManager = HttpContext.Current.GetOwinContext().GetBackOfficeSignInManager();
            var signInStatus = signInManager.PasswordSignInAsync(username, password, false, false).Result;
            var isValidUser = signInStatus == SignInStatus.Success;
            if (!isValidUser)
                throw new SecurityException();

            var memoryStream = new MemoryStream();
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("Data");

                var homeContent = Umbraco.ContentSingleAtXPath("//home");
                var maxLevel = homeContent.Descendants().Select(x => x.Level).Max();

                WriteHeaders(sheet, maxLevel);
                WriteContentToSheet(sheet, homeContent, maxLevel);

                for (int col = 1; col <= maxLevel + 2; col++)
                {
                    sheet.Column(col).AutoFit();
                }

                package.SaveAs(memoryStream);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(memoryStream)
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = $"full_content_{DateTime.UtcNow:yyyyMMddhhmmss}.xlsx"
            };

            return result;
        }

        private void WriteHeaders(ExcelWorksheet sheet, int maxLevel)
        {
            sheet.Cells[_rowNumber, 1].Value = "Page Node";
            for (var lv = 1; lv < maxLevel; lv++)
            {
                sheet.Cells[_rowNumber, lv + 1].Value = $"Page Child Node Level {lv}";
            }

            var pageTitleCol = maxLevel + 1;
            sheet.Cells[_rowNumber, pageTitleCol].Value = "Page Title";
            sheet.Cells[_rowNumber, pageTitleCol + 1].Value = "URL Link";

            _rowNumber++;
        }

        private void WriteContentToSheet(ExcelWorksheet sheet, IPublishedContent content, int maxLevel)
        {
            WriteContentToRow(sheet, content, maxLevel);

            foreach (var childContent in content.Children)
            {
                WriteContentToSheet(sheet, childContent, maxLevel);
            }
        }

        private void WriteContentToRow(ExcelWorksheet sheet, IPublishedContent content, int maxLevel)
        {
            sheet.Cells[_rowNumber, 1].Value = content.ContentType.Alias;

            var visitNode = content.Parent;
            for (int lv = content.Level - 1; lv >= 1; lv--)
            {
                sheet.Cells[_rowNumber, lv + 1].Value = GetPageTitle(visitNode);
                visitNode = visitNode.Parent;
            }

            var pageTitleCol = maxLevel + 1;
            sheet.Cells[_rowNumber, pageTitleCol].Value = GetPageTitle(content);
            sheet.Cells[_rowNumber, pageTitleCol + 1].Value = content?.Url;

            _rowNumber++;
        }

        private string GetPageTitle(IPublishedContent content)
        {
            if (content.ContentType.Alias.Equals("home"))
                return "home";

            return content.GetProperty("pageTitle")?.GetValue()?.ToString();
        }
    }
}