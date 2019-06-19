using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHCoApps.CMSWeb.Models
{
    public class SendGridEmailError
    {
        public SendGridEmailErrorDetail[] Errors { get; set; }
    }

    public class SendGridEmailErrorDetail
    {
        public string Message { get; set; }
        public string Field { get; set; }
        public string Help { get; set; }
    }
}