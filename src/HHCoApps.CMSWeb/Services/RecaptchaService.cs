using HHCoApps.CMSWeb.Models;
using Flurl.Http;
using System.Configuration;
using System.Net.Http;
using CoreForm = Umbraco.Forms.Core;

namespace HHCoApps.CMSWeb.Services
{
    public class RecaptchaService : IRecaptchaService
    {
        public bool VerifyRecaptcha(string token, decimal humanScore)
        {
            var secretKey = CoreForm.Configuration.GetSetting("RecaptchaPrivateKey");
            var captchaResponse = GetRecaptchaInfo(secretKey, token);

            return captchaResponse.Success && captchaResponse.Score >= humanScore;
        }

        private RecaptchaViewModelResponse GetRecaptchaInfo(string secret, string token)
        {
            var response = $"{ConfigurationManager.AppSettings["GoogleRecaptchaUrl"]}"
                .PostUrlEncodedAsync(new { secret, response = token }).Result;

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<RecaptchaViewModelResponse>().Result;

            return new RecaptchaViewModelResponse();
        }
    }

    public interface IRecaptchaService
    {
        bool VerifyRecaptcha(string token, decimal humanScore);
    }
}
