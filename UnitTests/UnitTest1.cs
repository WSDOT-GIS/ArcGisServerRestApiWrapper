using Esri.ArcGisServer.Rest;
using Esri.ArcGisServer.Rest.Authentication;
using Esri.ArcGisServer.Rest.Maps;
using Esri.ArcGisServer.Rest.Route;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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
        private Token _token;

        public Token Token
        {
            get
            {
                if (_token == null || _token.IsValid)
                {
                    string clientId = Settings.Default.ClientId;
                    string clientSecret = Settings.Default.ClientSecret;

                    // Make sure the default values aren't still in place. (I.e., app.config has not been created or customized.)
                    if (string.Compare(clientId, "PutYourClientIdHere", true) == 0)
                    {
                        Assert.Inconclusive("A ClientId value has not been specified in app.config.");
                    }
                    else if (string.Compare(clientSecret, "PutYourClientSecretHere", true) == 0)
                    {
                        Assert.Inconclusive("A ClientSecret value has not been specified in app.config.");
                    }

                    var authSvc = new AuthenticationService();
                    _token = authSvc.GetToken(clientId, clientSecret);
                }
                return _token;
            }
        }

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
            Token token = this.Token;
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
        public void TestDeserializeSolveResults()
        {
            // Get the Sample JSON from the assembly resources. This sample result comes from http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/.
            string json = Resources.SampleSolveResults;
            var solveResult = JsonConvert.DeserializeObject<SolveResult>(json);
            Assert.IsTrue(solveResult.directions.Length == 1, "There should be a single element in \"directions\".");
            var dir = solveResult.directions[0];
            Assert.AreEqual(dir.routeId, 1, "\"routeId\" should be 1.");
            Assert.IsInstanceOfType(dir.summary.envelope, typeof(Geometry));
            Assert.IsTrue(dir.summary.envelope.GetGeometryType() == GeometryType.esriGeometryEnvelope);
            Assert.AreEqual(dir.features.Length, 6, "There should be six features.");
            Assert.IsInstanceOfType(solveResult.routes, typeof(FeatureSet), "\"routes\" should be a FeatureSet.");
            Assert.IsTrue(solveResult.routes.features.Length == 1, "\"routes.features\" should contain a single feature.");
            Feature feature = solveResult.routes.features[0];
            Assert.IsInstanceOfType(feature.geometry, typeof(Geometry), "route feature should be a Polyline.");
            Assert.AreEqual(feature.geometry.GetGeometryType(), GeometryType.esriGeometryPolyline, "route feature should be a Polyline.");
        }

        /// <summary>
        /// Tests the "Finding the best route and driving directions between two locations" example from <see href="http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/"/>
        /// </summary>
        [TestMethod]
        public void TestSolve()
        {
            var svc = new RouteService();
            var solveParameters = new SolveParameters
            {
                stops = new double[][] { 
                    new double[] {-122.4079,37.78356},
                    new double[] {-122.404,37.782}
                },
                ////restrictionAttributeNames = new string[] { "none" }
            };
            SolveResult result = svc.Solve(solveParameters, this.Token);
            Assert.IsInstanceOfType(result, typeof(SolveResult));
            Assert.IsNotNull(result.directions, "Directions should not be null");
            Assert.AreEqual(result.directions.Length, 1, "Directions should have a single element.");
            Direction direction = result.directions.First();
            // Check some properties for expected results.
            Assert.AreEqual(direction.routeId, 1);
            Assert.AreEqual(direction.routeName, "Location 1 - Location 2");

            Assert.IsTrue(direction.features.Length > 1);

            Assert.AreEqual(direction.features.First().attributes["maneuverType"], "esriDMTDepart");
            Assert.AreEqual(direction.features.Last().attributes["maneuverType"], "esriDMTStop");

            Assert.IsNotNull(result.routes);
            Assert.AreEqual(result.routes.spatialReference.wkid, 4326);
        }

        /// <summary>
        /// Tests the "Finding the optimized route and driving directions to visit a set of locations" example from <see href="http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/"/>
        /// </summary>
        [TestMethod]
        public void TestSolve2()
        {
            const string stopsJson = "{\"features\":[{\"geometry\":{\"x\":-122.473948,\"y\":37.7436},\"attributes\":{\"Name\":\"Office\",\"Attr_TravelTime\":0}},{\"geometry\":{\"x\":-122.439613,\"y\":37.746144},\"attributes\":{\"Name\":\"Store 1\",\"Attr_TravelTime\":25}},{\"geometry\":{\"x\":-122.488254,\"y\":37.754092},\"attributes\":{\"Name\":\"Store 2\",\"Attr_TravelTime\":20}},{\"geometry\":{\"x\":-122.44915,\"y\":37.731837},\"attributes\":{\"Name\":\"Store 3\",\"Attr_TravelTime\":30}},{\"geometry\":{\"x\":-122.46441,\"y\":37.774756},\"attributes\":{\"Name\":\"Store 4\",\"Attr_TravelTime\":25}},{\"geometry\":{\"x\":-122.426896,\"y\":37.769352},\"attributes\":{\"Name\":\"Store 5\",\"Attr_TravelTime\":20}},{\"geometry\":{\"x\":-122.473948,\"y\":37.7436},\"attributes\":{\"Name\":\"Office\",\"Attr_TravelTime\":0}}]}";
            var stopFeatures = JsonConvert.DeserializeObject<FeatureSet>(stopsJson);
            Assert.AreEqual(stopFeatures.features.Length, 7, "There should be seven stops.");
            var solveParams = new SolveParameters
            {
                stopsAsFeatures = stopFeatures,
                findBestSequence = true,
                preserveFirstStop = true,
                preserveLastStop = true,
                returnDirections = true,
                returnRoutes = true
            };
            var svc = new RouteService();
            SolveResult result = svc.Solve(solveParams, this.Token);
            Assert.IsInstanceOfType(result, typeof(SolveResult));
            Assert.IsNotNull(result.routes, "Routes should not be null.");
            Assert.IsNotNull(result.directions, "Directions should not be null.");
        }
    }
}
