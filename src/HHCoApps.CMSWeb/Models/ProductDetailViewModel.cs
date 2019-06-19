using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Models
{
    public class ProductDetailViewModel
    {
        public TabItem[] Tabs { get; set; } = new TabItem[0];
    }
}