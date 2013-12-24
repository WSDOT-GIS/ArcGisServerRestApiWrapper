using Esri.ArcGisServer.Rest.Maps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnitTests.Properties;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestProperty("mapServiceUrl", "http://www.wsdot.wa.gov/geosvcs/ArcGIS/rest/services/Shared/WebBaseMapWebMercator/MapServer")]
        [TestMethod]
        public void TestExportMap()
        {
            var testProperties = MethodInfo.GetCurrentMethod().GetCustomAttributes<TestPropertyAttribute>().ToDictionary(k => k.Name, v => v.Value);
            var mapServiceUrl = testProperties["mapServiceUrl"];

            var parameters = new ExportMapParameters
            {
                BoundingBox = new double[] { 
                    -14011824.4072731,
                    5581676.67702371,
                    -12878110.4037477,
                    6375398.77873677
                },
                ResponseFormat = ExportMapResponseFormat.Image,
                ImageFormat = ExportMapImageFormat.Png,
                Dpi = 300,
                Size = new int[] { 600, 800 },
                Transparent = true
            };
            var mapService = new MapService { Uri = new Uri(mapServiceUrl) };
            Stream image = mapService.ExportMap(parameters);

            Assert.IsNotNull(image);

            using (FileStream fs = new FileStream("output.png", FileMode.Create))
            {
                image.CopyTo(fs);
            }
        }

        ////[TestMethod]
        ////public void TestAppConfig()
        ////{
        ////    string clientId = Settings.Default.ClientId;
        ////    string clientSecret = Settings.Default.ClientSecret;

        ////}
    }
}
