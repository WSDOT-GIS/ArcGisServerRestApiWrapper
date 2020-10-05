using Esri.ArcGisServer.Rest;
using Esri.ArcGisServer.Rest.Authentication;
using Esri.ArcGisServer.Rest.Geocode;
using Esri.ArcGisServer.Rest.Maps;
using Esri.ArcGisServer.Rest.Route;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
                if (_token == null || !_token.IsValid)
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


        [TestMethod]
        public void ConvertToValueForQueryString1()
        {
            List<string> stringListObject = new List<string>();
            stringListObject.Add("dogfood");
            stringListObject.Add("foobar");
            string output = Extensions.ConvertToValueForQueryString(stringListObject);
            Assert.AreEqual(output, "%5b%22dogfood%22%2c%22foobar%22%5d", output);
        }

        [TestMethod]
        public void GeocodeParametersToDictionary1()
        {
            GeocodeParameters gcParams = CreateTestGeocodeParamsSingleLineBasic();
            
            Dictionary<string, object> dictOutput = gcParams.ToDictionary();
            foreach (var entry in dictOutput)
            {
                string kvp = entry.Key + ": " + entry.Value;
                Console.WriteLine(kvp);
            }
            Assert.IsNotNull(dictOutput);
            Assert.IsInstanceOfType(dictOutput, typeof(Dictionary<string, object>));
        }

        [TestMethod]
        public void GeocodeParametersToQueryStringSingleLineBasic()
        {
            string singleLineBasicJson = "singleLine=614+E+Gorham+St&location=%5b-89.383%2c43.081%5d&outFields=NAME&forStorage=False&locationType=rooftop&f=json";
            GeocodeParameters gcParams = CreateTestGeocodeParamsSingleLineBasic();
            Dictionary<string, object> dictOutput = gcParams.ToDictionary();
            var test = dictOutput.ToQueryString();
            Assert.IsInstanceOfType(test, typeof(string), test);
            Assert.AreEqual(test, singleLineBasicJson);
        }

        [TestMethod]
        public void GeocodeParametersToQueryStringMultiLineBasic()
        {
            string multiLineBasicJson = "address=James+Madison+Park&address2=614+E+Gorham+St&address3=Basketball+courts&neighborhood=Downtown&city=Madison&subregion=Dane+County&region=WI&postal=53703&countryCode=USA&location=%5b-89.383%2c43.081%5d&forStorage=False&locationType=rooftop&f=json";
            GeocodeParameters gcParams = CreateTestGeocodeParamsMultiFieldBasic();
            Dictionary<string, object> dictOutput = gcParams.ToDictionary();
            var test = dictOutput.ToQueryString();
            Assert.IsInstanceOfType(test, typeof(string), test);
            Assert.AreEqual(test, multiLineBasicJson);
        }

        [TestMethod]
        public void GeocodeParametersToQueryStringSingleLineFull()
        {
            GeocodeParameters gcParams = CreateTestGeocodeParamsSingleLineFull();
            Dictionary<string, object> dictOutput = gcParams.ToDictionary();
            var test = dictOutput.ToQueryString();
            Assert.IsInstanceOfType(test, typeof(string), test);
        }

        [TestMethod]
        public void GeocodeParametersToQueryStringMultiLineFull()
        {
            GeocodeParameters gcParams = CreateTestGeocodeParamsMultiFieldFull();
            Dictionary<string, object> dictOutput = gcParams.ToDictionary();
            var test = dictOutput.ToQueryString();
            Assert.IsInstanceOfType(test, typeof(string), test);
        }

        private GeocodeParameters CreateTestGeocodeParamsSingleLineBasic()
        {
            GeocodeParameters gcParams = new GeocodeParameters();
            gcParams.singleLine = "614 E Gorham St";
            List<string> fields = new List<string>();
            fields.Add("NAME");
            gcParams.outFields = fields;
            gcParams.location = new double[] { -89.383, 43.081 };
            gcParams.forStorage = false;
            gcParams.locationType = LocationType.rooftop;
            
            return gcParams;
        }

        [TestMethod]
        public void DeserializeFindAddressCandidatesSuccessResults()
        {
            // Get the Sample JSON from the assembly resources.
            string json = Resources.SampleGeocodeSuccessResults;
            var gcResult = JsonConvert.DeserializeObject<GeocodeResult>(json);
            Assert.IsNotNull(gcResult.spatialReference, "Result should include spatialReference");
            Assert.AreEqual(gcResult.spatialReference.wkid, 102100, "response should have wkid=102100");
            Assert.IsTrue(gcResult.candidates.Length == 2, "There should be 2 elements in \"candidates\".");
            var result1 = gcResult.candidates[0];
            Assert.AreEqual(result1.address, "James Madison Park", "Address should be \"James Madison Park\".");
            Assert.AreEqual(result1.location.x, -9950097.875447867, "x coordinate should be -9950097.875447867.");
            Assert.AreEqual(result1.location.y, 5324273.956367552, "x coordinate should be 5324273.956367552.");
            Assert.AreEqual(result1.score, 72.14, "score should be 72.14");
            var attributes = result1.attributes;
            Assert.IsInstanceOfType(result1.attributes, typeof(Dictionary<string, object>));
            Assert.AreEqual(result1.attributes["Addr_type"], "POI", "Addr_type should be \"POI\"");
            Assert.AreEqual(result1.attributes["StAddr"], "500 E Gorham St", "StAddr should be \"500 E Gorham St\"");
            Assert.AreEqual(result1.attributes["Nbrhd"], "Old Market Place", "Nbrhd should be \"Old Market Place\"");
            Assert.AreEqual(result1.attributes["DisplayX"], -89.38324999999998, "DisplayX should be -89.38324999999998");
            Assert.IsInstanceOfType(result1.extent, typeof(Geometry));
        }

        [TestMethod]
        public void DeserializeFindAddressCandidatesErrorResults()
        {
            // Get the Sample JSON from the assembly resources.
            string json = Resources.SampleGeocodeErrorResults;
            var errorResult = JsonConvert.DeserializeObject<GeocodeErrorResponse>(json);
            Assert.IsTrue(errorResult.error.code == 499, "error code should be 499");
            Assert.IsNotNull(errorResult.error.message, "There should be an error message.");
            Assert.AreEqual(errorResult.error.message, "Token required but not passed in the request.", "Error message should match");
            string details = errorResult.error.details[0];
            Assert.AreEqual(details, "Token required.", "Should say 'Token Required.'");
        }



        /// <summary>
        /// Test the FindAddressCandidates method using a simple single-line address
        /// </summary>
        [TestMethod]
        public void FindAddressCandidatesSingleLineBasic()
        {
            string singleLineBasicJson = "singleLine=614+E+Gorham+St&location=%5b-89.383%2c43.081%5d&outFields=NAME&forStorage=False&locationType=rooftop&f=json";
            GeocodeParameters gcParams = CreateTestGeocodeParamsSingleLineBasic();
            Dictionary<string, object> dictOutput = gcParams.ToDictionary();
            string queryString = dictOutput.ToQueryString();
            Assert.IsInstanceOfType(queryString, typeof(string), queryString);
            Assert.AreEqual(queryString, singleLineBasicJson);

            GeocodeService service = new GeocodeService();
            GeocodeResult result = service.FindAddressCandidates(gcParams, this.Token);
            Assert.IsInstanceOfType(result, typeof(GeocodeResult));
            Assert.IsNotNull(result.candidates, "Candidates should not be null.");
            Assert.IsNotNull(result.spatialReference, "Spatial Reference should not be null.");
        }

        /// <summary>
        /// Test the FindAddressCandidates method using a single-line address and full parameter list
        /// </summary>
        [TestMethod]
        public void FindAddressCandidatesSingleLineFull()
        {
            GeocodeParameters gcParams = CreateTestGeocodeParamsSingleLineFull();
            Dictionary<string, object> dictOutput = gcParams.ToDictionary();
            var test = dictOutput.ToQueryString();
            Assert.IsInstanceOfType(test, typeof(string), test);

            GeocodeService service = new GeocodeService();
            GeocodeResult result = service.FindAddressCandidates(gcParams, this.Token);
            Assert.IsInstanceOfType(result, typeof(GeocodeResult));
            Assert.IsNotNull(result.candidates, "Candidates should not be null.");
            Assert.IsNotNull(result.spatialReference, "Spatial Reference should not be null.");
        }

        /// <summary>
        /// Test the FindAddressCandidates method using a simple multi-line address
        /// </summary>
        [TestMethod]
        public void FindAddressCandidatesMultiLineBasic()
        {
            string multiLineBasicJson = "address=James+Madison+Park&address2=614+E+Gorham+St&address3=Basketball+courts&neighborhood=Downtown&city=Madison&subregion=Dane+County&region=WI&postal=53703&countryCode=USA&location=%5b-89.383%2c43.081%5d&forStorage=False&locationType=rooftop&f=json";
            GeocodeParameters gcParams = CreateTestGeocodeParamsMultiFieldBasic();
            Dictionary<string, object> dictOutput = gcParams.ToDictionary();
            var test = dictOutput.ToQueryString();
            Assert.IsInstanceOfType(test, typeof(string), test);
            Assert.AreEqual(test, multiLineBasicJson);

            GeocodeService service = new GeocodeService();
            GeocodeResult result = service.FindAddressCandidates(gcParams, this.Token);
            Assert.IsInstanceOfType(result, typeof(GeocodeResult));
            Assert.IsNotNull(result.candidates, "Candidates should not be null.");
            Assert.IsNotNull(result.spatialReference, "Spatial Reference should not be null.");
        }

        /// <summary>
        /// Test the FindAddressCandidates method using a single-line address and full parameter list
        /// </summary>
        [TestMethod]
        public void FindAddressCandidatesMultiLineFull()
        {
            GeocodeParameters gcParams = CreateTestGeocodeParamsMultiFieldFull();
            Dictionary<string, object> dictOutput = gcParams.ToDictionary();
            var test = dictOutput.ToQueryString();
            Assert.IsInstanceOfType(test, typeof(string), test);

            GeocodeService service = new GeocodeService();
            GeocodeResult result = service.FindAddressCandidates(gcParams, this.Token);
            Assert.IsInstanceOfType(result, typeof(GeocodeResult));
            Assert.IsNotNull(result.candidates, "Candidates should not be null.");
            Assert.IsNotNull(result.spatialReference, "Spatial Reference should not be null.");
        }

        /// <summary>
        /// Test the FindAddressCandidates method using a simple single-line address without a token
        /// </summary>
        [TestMethod]
        public void FindAddressCandidatesWithoutTokenReturnsSuccess()
        {
            GeocodeParameters gcParams = CreateTestGeocodeParamsSingleLineBasic();
            GeocodeService service = new GeocodeService();
            GeocodeResult result = service.FindAddressCandidates(gcParams);
            Assert.IsInstanceOfType(result, typeof(GeocodeResult));
            Assert.IsNotNull(result.candidates, "Candidates should not be null.");
            Assert.IsNotNull(result.spatialReference, "Spatial Reference should not be null.");
        }

        /// <summary>
        /// Test the FindAddressCandidates method using a simple single-line address for storage without a token
        /// </summary>
        [TestMethod]
        public void FindAddressCandidatesWithoutTokenForStorageThrowsException()
        {
            GeocodeParameters gcParams = CreateTestGeocodeParamsSingleLineBasic();
            gcParams.forStorage = true;
            GeocodeService service = new GeocodeService();
            try
            {
                GeocodeResult result = service.FindAddressCandidates(gcParams);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(GeocodeException));
            }
        }


        private GeocodeParameters CreateTestGeocodeParamsSingleLineFull()
        {
            GeocodeParameters gcParams = CreateTestGeocodeParamsSingleLineBasic();
            gcParams.searchExtent = new double[] { -90, 43.2, -89, 43.0 };
            List<string> categories = new List<string>();
            categories.Add("Address");
            categories.Add("Residence");
            categories.Add("Park");
            gcParams.category = categories;
            SpatialReference outSR = new SpatialReference();
            outSR.wkid = 102100;
            gcParams.outSRAsObject = outSR;
            List<string> fields = new List<string>();
            fields.Add("*");
            gcParams.outFields = fields;
            gcParams.maxLocations = 2;
            gcParams.forStorage = true;
            gcParams.matchOutOfRange = false;
            gcParams.langCode = "ES";
            List<string> sourceCountries = new List<string>();
            sourceCountries.Add("USA");
            sourceCountries.Add("CAN");
            gcParams.sourceCountry = sourceCountries;
            
            return gcParams;
        }

        private GeocodeParameters CreateTestGeocodeParamsMultiFieldBasic()
        {
            GeocodeParameters gcParams = new GeocodeParameters();
            gcParams.address = "James Madison Park";
            gcParams.address2 = "614 E Gorham St";
            gcParams.address3 = "Basketball courts";
            gcParams.neighborhood = "Downtown";
            gcParams.city = "Madison";
            gcParams.subregion = "Dane County";
            gcParams.region = "WI";
            gcParams.postal = "53703";
            gcParams.postalExt = null;
            gcParams.countryCode = "USA";
            gcParams.location = new double[] { -89.383, 43.081 };
            gcParams.forStorage = false;
            gcParams.locationType = LocationType.rooftop;

            return gcParams;
        }

        private GeocodeParameters CreateTestGeocodeParamsMultiFieldFull()
        {
            GeocodeParameters gcParams = CreateTestGeocodeParamsMultiFieldBasic();
            gcParams.searchExtent = new double[] { -90, 43.2, -89, 43.0 };
            List<string> categories = new List<string>();
            categories.Add("Address");
            categories.Add("Residence");
            categories.Add("Park");
            gcParams.category = categories;
            SpatialReference outSR = new SpatialReference();
            outSR.wkid = 102100;
            gcParams.outSRAsObject = outSR;
            List<string> fields = new List<string>();
            fields.Add("*");
            gcParams.outFields = fields;
            gcParams.maxLocations = 2;
            gcParams.forStorage = true;
            gcParams.matchOutOfRange = false;
            gcParams.langCode = "ES";
            List<string> sourceCountries = new List<string>();
            sourceCountries.Add("USA");
            sourceCountries.Add("CAN");
            gcParams.sourceCountry = sourceCountries;

            return gcParams;
        }
    }
}
