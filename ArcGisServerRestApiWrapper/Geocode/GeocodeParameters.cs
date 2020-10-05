using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.ArcGisServer.Rest.Geocode
{
    /// <summary>
    /// Parameters for use with the World Geocoding Service findAddressCandidates operation. <see href="https://developers.arcgis.com/rest/geocode/api-reference/geocoding-find-address-candidates.htm"/>
    /// </summary>
    public class GeocodeParameters
    {
        /// <summary>
        /// <para>Specifies the location to be geocoded as a single string text. This can be a street address, place name, postal code, or POI.</para>
        /// <para>Alternatively, provide location to be geocoded using the multi-field parameters of <see cref="GeocodeParameters.address"/>, <see cref="GeocodeParameters.neighborhood"/>, <see cref="GeocodeParameters.city"/>, <see cref="GeocodeParameters.subregion"/>, <see cref="GeocodeParameters.region"/>, <see cref="GeocodeParameters.postal"/>, <see cref="GeocodeParameters.postalExt"/>, and <see cref="GeocodeParameters.countryCode"/></para>
        /// </summary>
        /// <example>Street address <c>380 New York St, Redlands, California 92373</c></example>
        /// <example>Beetham Tower, 301 Deansgate, Suite 4208, Manchester, England</example>
        public string singleLine { get; set; }

        /// <summary>
        /// <para>Specify parts of an address separately using the multi-field parameters of <see cref="GeocodeParameters.address"/>, <see cref="GeocodeParameters.address2"/>, <see cref="GeocodeParameters.address3"/>, <see cref="GeocodeParameters.neighborhood"/>, <see cref="GeocodeParameters.city"/>, <see cref="GeocodeParameters.subregion"/>, <see cref="GeocodeParameters.region"/>, <see cref="GeocodeParameters.postal"/>, <see cref="GeocodeParameters.postalExt"/>, and <see cref="GeocodeParameters.countryCode"/>.</para>
        /// <para>The full street address of a place (excluding administrative divisions and postal codes) may consist of multiple components, such as building name, street, and subunit (apartment).</para>
        /// <para>Three different address parameters can be used to represent the different components of a street address: <see cref="GeocodeParameters.address"/>, <see cref="GeocodeParameters.address2"/>, and <see cref="GeocodeParameters.address3"/>. For most geocoding cases it will only be necessary to use the address parameter.</para>
        /// <para>If you want to geocode the address Beetham Tower, 301 Deansgate, Suite 4208, Manchester, England using multiple input fields, you can set <see cref="GeocodeParameters.address"/>=Beetham Tower, <see cref="GeocodeParameters.address2"/>=301 Deansgate, and <see cref="GeocodeParameters.address3"/>=Suite 4208.</para>
        /// <para>Specifies the location to be geocoded as a single string text. This can be a street address, place name, postal code, or POI.</para>
        /// </summary>
        /// <example>Beetham Tower</example>
        public string address { get; set; }

        /// <summary>
        /// A string that represents the second line of a street address. This can include street name/house number, building name, place name, or subunit.
        /// </summary>
        /// <example>301 Deansgate</example>
        public string address2 { get; set; }

        /// <summary>
        /// A string that represents the third line of a street address. This can include street name/house number, building name, place name, or subunit.
        /// </summary>
        /// <example>Suite 4208</example>
        public string address3 { get; set; }

        /// <summary>
        /// The smallest administrative division associated with an address, typically, a neighborhood or a section of a larger populated place. A neighborhood is a subdivision of a city.
        /// <para>The neighborhood parameter is not used in all countries or regions.</para>
        /// </summary>
        public string neighborhood { get; set; }

        /// <summary>
        /// The next largest administrative division associated with an address, typically, a city or municipality. A city is a subdivision of a subregion or a region.
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// The next largest administrative division associated with an address. Depending on the country, a subregion can represent a county, state, or province.
        /// </summary>
        public string subregion { get; set; }

        /// <summary>
        /// The largest administrative division associated with an address, typically, a state or province.
        /// </summary>
        public string region { get; set; }

        /// <summary>
        /// The standard postal code for an address, typically, a 3–6-digit alphanumeric code.
        /// </summary>
        public string postal { get; set; }

        /// <summary>
        /// A postal code extension, such as the United States Postal Service ZIP+4 code, provides finer resolution or higher accuracy when also passing postal.
        /// </summary>
        public string postalExt { get; set; }

        /// <summary>
        /// <para>The part of a multi-field address representing the country. Providing this value increases geocoding speed. Acceptable values include the full country name in English or the official language of the country, the ISO 3166-1 2-digit country code, or the ISO 3166-1 3-digit country code.</para>
        /// <para>A list of supported countries and codes is available <see href="https://developers.arcgis.com/rest/geocode/api-reference/geocode-coverage.htm">here</see></para>.
        /// </summary>
        /// <remarks>The <see cref="GeocodeParameters.sourceCountry"/> and <see cref="GeocodeParameters.countryCode"/> parameters are similar but serve different purposes. The <see cref="GeocodeParameters.countryCode"/> parameter defines the country value for a multifield geocode request. The <see cref="GeocodeParameters.sourceCountry"/> parameter defines the country value for a request regardless of whether it is a single-field or multifield request. 
        /// If both <see cref="GeocodeParameters.sourceCountry"/> and <see cref="GeocodeParameters.countryCode"/> are included in a findAddressCandidates request, and the country values are different, the <see cref="GeocodeParameters.countryCode"/> value takes priority over <see cref="GeocodeParameters.sourceCountry"/>.</remarks>
        public string countryCode { get; set; }

        /// <summary>
        /// The geocoding operation retrieves results quicker when you pass in valid singleLine and magicKey values than when you don't pass in magicKey. However, to get these advantages, you need to make a prior request to suggest, which provides a magicKey.
        /// </summary>
        public string magicKey { get; set; }

        /// <summary>
        /// A set of bounding box coordinates that limit the search area to a specific region. 
        /// This is especially useful for applications in which a user will search for places and addresses only within the current map extent. 
        /// <para>Coordinates should be listed in the following order: xmin, ymin, xmax, ymax - Assumed to be in the same spatial reference system as the geocode service. </para>
        /// </summary>
        public double[] searchExtent { get; set; }

        /// <summary>
        /// Use this parameter instead of <see cref="GeocodeParameters.searchExtent"/> if you need to specify a bounding box in a different coordinate system
        /// than the one used by the geocode service.
        /// A set of bounding box coordinates that limit the search area to a specific region. 
        /// This is especially useful for applications in which a user will search for places and addresses only within the current map extent. 
        /// </summary>
        public Geometry searchExtentAsGeometry { get; set; }
        
        /// <summary>
        /// <para>Defines an origin point location that is used with the distance parameter to sort geocoding candidates based upon their proximity to the location. 
        /// The priority of candidates within a default radial distance from the location are boosted relative to those outside the radius. 
        /// The default distance is 50,000 meters; this value is not configurable.</para>
        /// <para>This is useful in mobile applications where a user will want to search for places in the vicinity of their current GPS location; the location parameter can be used in this scenario.</para>
        /// <para>Specify in order of X (longitude) followed by Y (latitude).</para>
        /// </summary>
        /// <remarks>Assumed to be in the same spatial reference system as the geocode service.</remarks>
        public double[] location { get; set; }

        /// <summary>
        /// <para>Defines an origin point location that is used with the distance parameter to sort geocoding candidates based upon their proximity to the location. 
        /// The priority of candidates within a default radial distance from the location are boosted relative to those outside the radius. 
        /// The default distance is 50,000 meters; this value is not configurable.</para>
        /// <para>This is useful in mobile applications where a user will want to search for places in the vicinity of their current GPS location; the location parameter can be used in this scenario.</para>
        /// </summary>
        public Geometry locationAsGeometry { get; set; }

        /// <summary>
        /// One or more place or address types that can be used to filter findAddressCandidates results.
        /// <para>A list of supported categories is available in the 
        /// <see href="https://developers.arcgis.com/rest/geocode/api-reference/geocoding-category-filtering.htm#ESRI_SECTION1_502B3FE2028145D7B189C25B1A00E17B">
        /// Category filtering page of the geocode service documentation</see>.</para>
        /// <para>Use category filtering to limit matches to specific place types or address levels, avoid fallback matches to unwanted address levels, or disambiguate coordinate searches.</para>
        /// </summary>
        public IEnumerable<string> category { get; set; }

        /// <summary>
        /// The spatial reference of the x/y coordinates returned by a geocode request as a WKID. This is useful for applications using a map with a spatial reference different than that of the geocode service. 
        /// </summary>
        /// <example>The World Geocoding Service spatial reference is WGS84 whose WKID = 4326.</example>
        public int? outSR { get; set; }
        /// <summary>
        /// The spatial reference of the x/y coordinates returned by a geocode request. This is useful for applications using a map with a spatial reference different than that of the geocode service. 
        /// </summary>
        public SpatialReference outSRAsObject { get; set; }

        /// <summary>
        /// <para>The list of fields to be returned in the response. Field names are case-sensitive. 
        /// Descriptions for each of these fields are available in the 
        /// <see href="https://developers.arcgis.com/rest/geocode/api-reference/geocoding-service-output.htm#ESRI_SECTION1_42D7D3D0231241E9B656C01438209440">
        /// Output fields section of the geocode service documentation</see></para>. 
        /// <para>The returned address, x/y coordinates of the match location, match score, spatial reference, extent of the output feature, and the addr_type (match level) are returned by default. </para>
        /// <para>Use a single value of "*" to specify that all fields are to be returned.</para>
        /// </summary>
        public IEnumerable<string> outFields { get; set; }

        /// <summary>
        /// <para>The maximum number of locations to be returned by a search, up to the maximum number allowed by the service. If not specified, then all matching candidates up to the service maximum are returned.</para>
        /// <para>The world geocoding service allows up to 50 candidates to be returned for a single request. </para>
        /// </summary>
        public int? maxLocations { get; set; }

        /// <summary>
        /// <para>Specifies whether the results of the operation will be persisted.</para>
        /// <para>The default value is false, which indicates the results of the operation can't be stored, but they can be temporarily displayed on a map for instance. 
        /// If you store the results, in a database, for example, you need to set this parameter to true.</para>
        /// <para>Applications are contractually prohibited from storing the results of geocoding transactions unless 
        /// they make the request by passing the forStorage parameter with a value of true and the token parameter with a valid ArcGIS Online token.
        /// Instructions for composing a request with a valid token are provided in the 
        /// <see href="https://developers.arcgis.com/rest/geocode/api-reference/geocoding-authenticate-a-request.htm">authentication topic</see> of the geocode service documentation.</para>
        /// <para>ArcGIS Online service credits are deducted from the organization account for each geocode transaction 
        /// which includes the forStorage parameter with a value of true and a valid token. 
        /// Refer to the <see href="http://www.esri.com/software/arcgis/arcgisonline/credits">ArcGIS Online service credits overview page</see> 
        /// for more information about how credits are charged.</para>
        /// <para>The default value for the forStorage parameter is "false".</para>
        /// </summary>
        public bool? forStorage { get; set; }

        /// <summary>
        /// Specifies if StreetAddress matches should be returned even when the input house number is outside of the house number range defined for the input street. 
        /// <para>Out-of-range matches have <c>Addr_type=StreetAddressExt</c>. The geometry of such matches is a point corresponding to the end of the street segment where the range value is closest to the input house number. If matchOutOfRange is not specified in a request, its value is set to true by default.</para>
        /// </summary>
        /// <para>Input house numbers that exceed the range on a street segment by more than 100 will not result in StreetAddressExt matches. 
        /// For streets with smaller house number ranges, the maxOutOfRange tolerance is less than 100.</para>
        public bool? matchOutOfRange { get; set; }

        /// <summary>
        /// Specifies if the output geometry of PointAddress matches should be the rooftop point or street entrance location. 
        /// <para>Valid values are rooftop and street. The default value is street.</para>
        /// </summary>
        public LocationType? locationType { get; set; }

        /// <summary>
        /// Sets the language in which geocode results are returned. Addresses and places in many countries are available in more than one language; in these cases the langCode parameter can be used to specify which language should be used for results returned by the findAddressCandidates operation. 
        /// This is useful for ensuring that results are returned in the expected language. For example, a web application could be designed to get the browser language and pass it as the langCode parameter value in a findAddressCandidates request.
        /// <para>See the Supported Language Codes column of the <see href="https://developers.arcgis.com/rest/geocode/api-reference/geocode-coverage.htm#GUID-D61FB53E-32DF-4E0E-A1CC-473BA38A23C0">table of supported countries</see> for valid language code values in each country. </para>
        /// </summary>
        public string langCode { get; set; }

        /// <summary>
        /// <para>A list of 3-character country codes. Limits the candidates returned to the specified country or countries.</para>
        /// <para>Acceptable values include the ISO 3166-1 3-character country code. You can specify multiple country codes to limit results to more than one country.</para>
        /// <para>A list of supported countries and codes is available <a href="https://developers.arcgis.com/rest/geocode/api-reference/geocode-coverage.htm">here</a></para>.
        /// </summary>
        /// <example>USA</example>
        /// <remarks>The <see cref="GeocodeParameters.sourceCountry"/> and <see cref="GeocodeParameters.countryCode"/> parameters are similar but serve different purposes. The <see cref="GeocodeParameters.countryCode"/> parameter defines the country value for a multifield geocode request. The <see cref="GeocodeParameters.sourceCountry"/> parameter defines the country value for a request regardless of whether it is a single-field or multifield request. If both <see cref="GeocodeParameters.sourceCountry"/> and <see cref="GeocodeParameters.countryCode"/> are included in a findAddressCandidates request, and the country values are different, the <see cref="GeocodeParameters.countryCode"/> value takes priority over <see cref="GeocodeParameters.sourceCountry"/>.</remarks>
        public IEnumerable<string> sourceCountry { get; set; }



        /// <summary>
        /// Converts the object into a dictionary.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionary()
        {
            var output = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(this.singleLine))
            {
                output["singleLine"] = this.singleLine; 
            }
            if (!string.IsNullOrWhiteSpace(this.address))
            {
                output["address"] = this.address;
            }
            if (!string.IsNullOrWhiteSpace(this.address2))
            {
                output["address2"] = this.address2;
            }
            if (!string.IsNullOrWhiteSpace(this.address3))
            {
                output["address3"] = this.address3;
            }
            if (!string.IsNullOrWhiteSpace(this.neighborhood))
            {
                output["neighborhood"] = this.neighborhood;
            }
            if (!string.IsNullOrWhiteSpace(this.city))
            {
                output["city"] = this.city;
            }
            if (!string.IsNullOrWhiteSpace(this.subregion))
            {
                output["subregion"] = this.subregion;
            }
            if (!string.IsNullOrWhiteSpace(this.region))
            {
                output["region"] = this.region;
            }
            if (!string.IsNullOrWhiteSpace(this.postal))
            {
                output["postal"] = this.postal;
            }
            if (!string.IsNullOrWhiteSpace(this.postalExt))
            {
                output["postalExt"] = this.postalExt;
            }
            if (!string.IsNullOrWhiteSpace(this.countryCode))
            {
                output["countryCode"] = this.countryCode;
            }
            if (!string.IsNullOrWhiteSpace(this.magicKey))
            {
                output["magicKey"] = this.magicKey;
            }
            output.AddFirstNonNullValue("searchExtent", this.searchExtent, this.searchExtentAsGeometry);
            output.AddFirstNonNullValue("location", this.location, this.locationAsGeometry);
            if (this.category != null)
            {
                output.Add("category", string.Join(",", this.category));
            }
            output.AddFirstNonNullValue("outSR", this.outSR, this.outSRAsObject);
            if (this.outFields != null && this.outFields.Count() > 0)
            {
                output.Add("outFields", string.Join(",", this.outFields));
            }
            if (this.maxLocations.HasValue) output.Add("maxLocations", this.maxLocations);
            if (this.forStorage.HasValue) output.Add("forStorage", this.forStorage);
            if (this.matchOutOfRange.HasValue) output.Add("matchOutOfRange", this.matchOutOfRange);
            if (this.locationType.HasValue) output.Add("locationType", this.locationType.Value);
            if (!string.IsNullOrWhiteSpace(this.langCode))
            {
                output["langCode"] = this.langCode;
            }
            if (this.sourceCountry != null && this.sourceCountry.Count() > 0)
            {
                output.Add("sourceCountry", string.Join(",", this.sourceCountry));
            }
            
            output.Add("f", "json");

            return output;
        }

        /// <summary>
        /// Get the address parts as 1 string.
        /// </summary>
        /// <returns>Returns singleLine if not null, otherwise returns all multi-field address parameters separated by a space.</returns>
        public string GetAddressString()
        {
            if (!string.IsNullOrWhiteSpace(this.singleLine))
            {
                return this.singleLine;
            }
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(this.address))
            {
                sb.Append(this.address);
            }
            if (!string.IsNullOrWhiteSpace(this.address2))
            {
                sb.Append(" " + this.address2);
            }
            if (!string.IsNullOrWhiteSpace(this.address3))
            {
                sb.Append(" " + this.address3);
            }
            if (!string.IsNullOrWhiteSpace(this.neighborhood))
            {
                sb.Append(" " + this.neighborhood);
            }
            if (!string.IsNullOrWhiteSpace(this.city))
            {
                sb.Append(" " + this.city);
            }
            if (!string.IsNullOrWhiteSpace(this.subregion))
            {
                sb.Append(" " + this.subregion);
            }
            if (!string.IsNullOrWhiteSpace(this.region))
            {
                sb.Append(" " + this.region);
            }
            if (!string.IsNullOrWhiteSpace(this.postal))
            {
                sb.Append(" " + this.postal);
            }
            if (!string.IsNullOrWhiteSpace(this.postalExt))
            {
                sb.Append(" " + this.postalExt);
            }
            if (!string.IsNullOrWhiteSpace(this.countryCode))
            {
                sb.Append(" " + this.countryCode);
            }
            return sb.ToString();
        }
    }
}
