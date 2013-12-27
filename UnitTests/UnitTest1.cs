using Esri.ArcGisServer.Rest;
using Esri.ArcGisServer.Rest.Authentication;
using Esri.ArcGisServer.Rest.Maps;
using Esri.ArcGisServer.Rest.Route;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Text;
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

        /// <summary>
        /// Tests the function that gets a token.
        /// </summary>
        [TestMethod]
        public void TestGetToken()
        {
            string clientId = Settings.Default.ClientId;
            string clientSecret = Settings.Default.ClientSecret;

            // Make sure the default values aren't still in place. (I.e., app.config has not been created or customized.)
            if (string.Compare(clientId, "PutYourClientIdHere", true) == 0) {
                Assert.Inconclusive("A ClientId value has not been specified in app.config.");
            }
            else if (string.Compare(clientSecret, "PutYourClientSecretHere", true) == 0)
            {
                Assert.Inconclusive("A ClientSecret value has not been specified in app.config.");
            }

            var authSvc = new AuthenticationService();
            Token token = null;
            try
            {
                token = authSvc.GetToken(clientId, clientSecret);
            }
            catch (GetTokenException ex)
            {
                Assert.Fail("An exception occured.{0}", ex.ToString());
            }
            Assert.IsNotNull(token);
            Assert.IsFalse(string.IsNullOrWhiteSpace(token.AccessToken));
            Assert.IsTrue(token.IsValid);

        }

        /// <summary>
        /// Tests the function that gets a token with bad client ID and client secret.
        /// </summary>
        [TestMethod]
        public void TestGetTokenException()
        {
            var authSvc = new AuthenticationService();
            Token token = null;
            try
            {
                token = authSvc.GetToken("badid", "invalidsecret");
            }
            catch (GetTokenException ex)
            {
                Assert.IsNotNull(ex);
            }

        }

        [TestMethod]
        public void DeserializeSolveResults()
        {
            // Get the Sample JSON from the assembly resources. This sample result comes from http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/.
            string json = Resources.SampleSolveResults;
            var solveResult = JsonSerializer.DeserializeFromString<SolveResult>(json);
            Assert.IsTrue(solveResult.directions.Length == 1, "There should be a single element in \"directions\".");
            var dir = solveResult.directions[0];
            Assert.AreEqual(dir.routeId, 1, "\"routeId\" should be 1.");
            Assert.IsInstanceOfType(dir.summary.envelope, typeof(Geometry));
            Assert.IsTrue(dir.summary.envelope.GetType() == GeometryType.Envelope);
            Assert.AreEqual(dir.features.Length, 6, "There should be six features.");
            Assert.IsInstanceOfType(solveResult.routes, typeof(FeatureSet), "\"routes\" should be a FeatureSet.");
            Assert.IsTrue(solveResult.routes.features.Length == 1, "\"routes.features\" should contain a single feature.");
            Feature feature = solveResult.routes.features[0];
            Assert.IsInstanceOfType(feature.geometry, typeof(Geometry), "route feature should be a Polyline.");
            Assert.AreEqual(feature.geometry.GetType(), GeometryType.Polyline, "route feature should be a Polyline.");
        }
    }
}
