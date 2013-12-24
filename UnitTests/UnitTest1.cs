using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Esri.ArcGisServer.Rest.Maps;
using System.IO;
using System.Reflection;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestProperty("mapServiceUrl", "http://www.wsdot.wa.gov/geosvcs/ArcGIS/rest/services/Shared/WebBaseMapWebMercator/MapServer")]
        [TestMethod]
        public void TestExportMap()
        {
            var testProperties = MethodInfo.GetCurrentMethod().GetCustomAttributes<TestPropertyAttribute>();
            var mapServiceUrl = (from p in testProperties
                                where p.Name == "mapServiceUrl"
                                select p.Value).Single();

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


            ////mapService.ExportMapCompleted += new EventHandler<MapExportCompletedEventArgs>(mapService_ExportMapCompleted);

            ////IAsyncResult asyncResult = mapService.BeginExportMap(parameters);

            ////do
            ////{

            ////} while (!finished);
        }

        ////static void mapService_ExportMapCompleted(object sender, MapExportCompletedEventArgs e)
        ////{
        ////    using (var stream = e.ResponseStream)
        ////    {
        ////        using (FileStream fs = new FileStream("output.png", FileMode.Create))
        ////        {
        ////            stream.CopyTo(fs);
        ////        }
        ////    }
        ////    finished = true;
        ////}
    }
}
