using AutoMapper;
using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HHCoApps.CMSWeb.Caching;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;
using Umbraco.Web.WebApi;
using WebApi.OutputCache.V2;

namespace HHCoApps.CMSWeb.Controllers
{
    [JsonCamelCaseFormatter]
    public class StoresLocatorController : UmbracoApiController
    {
        private const int DefaultMaximumStore = 15;
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IMapper _mapper;

        public StoresLocatorController(IMapper mapper, UmbracoHelper umbracoHelper)
        {
            _mapper = mapper;
            _umbracoHelper = umbracoHelper;
        }

        public StoreConfigurationViewModel GetMapSettings(string id)
        {
            if (string.IsNullOrEmpty(id)) 
                return new StoreConfigurationViewModel();

            var apiKey = _umbracoHelper.Content<Stores>(id).APikey;
            var maxStores = _umbracoHelper.Content<Stores>(id).NumberOfStoresShown;
            var defaultCoordinates = _umbracoHelper.Content<Stores>(id).DefaultCoordinates;
            var defaultCountryCode = _umbracoHelper.Content<Stores>(id).DefaultCountry;
            var defaultZoomLevel = _umbracoHelper.Content<Stores>(id).DefaultZoomLevel;
            var storeConfiguration = new StoreConfigurationViewModel
            {
                ApiKey = apiKey,
                MaxStoresShown = maxStores,
                DefaultCoordinates = defaultCoordinates,
                DefaultCountryCode = defaultCountryCode,
                DefaultZoomLevel = defaultZoomLevel
            };
            return storeConfiguration;
        }

        [HttpGet]
        public IHttpActionResult SearchStores([FromUri] double latitude, [FromUri] double longitude, [FromUri] string id, [FromUri] int numberOfStoresForDisplay = DefaultMaximumStore)
        {
            if (string.IsNullOrEmpty(id)) 
                return BadRequest();

            var result = new GeoJson
            {
                Type = "FeatureCollection",
                Features = new List<StoreInformation>()
            };

            var storesPage = _umbracoHelper.Content<Stores>(id);
            var stores = storesPage.Children<Store>();
            var radius = storesPage.RadiusRangeMeters == default ? 10000 : storesPage.RadiusRangeMeters;

            if (result.Features.Any())
            {
                result.Features.Sort((a, b) => a.Properties.DistanceFromOrigin.CompareTo(b.Properties.DistanceFromOrigin));
                result.Features = result.Features.Take(numberOfStoresForDisplay).ToList();
            }

            return Ok(result);
        }

        private double GetDistanceBetweenTwoLocations(double lat1, double lon1, double lat2, double lon2)
        {
            var theta = lon1 - lon2;
            var dist = Math.Sin(DegToRad(lat1)) * Math.Sin(DegToRad(lat2)) + Math.Cos(DegToRad(lat1)) * Math.Cos(DegToRad(lat2)) * Math.Cos(DegToRad(theta));

            dist = Math.Acos(dist);
            dist = RadToDeg(dist);
            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344;

            return dist;
        }

        private double DegToRad(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        private double RadToDeg(double rad)
        {
            return rad / Math.PI * 180.0;
        }
    }
}
