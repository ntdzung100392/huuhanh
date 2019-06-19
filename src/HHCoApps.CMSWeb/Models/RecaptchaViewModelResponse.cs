using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHCoApps.CMSWeb.Models
{
    public class RecaptchaViewModelResponse
    {
        public bool Success { get; set; }
        public decimal Score { get; set; }
        public string Action { set; get; }
        public string HostName { set; get; }
    }
}