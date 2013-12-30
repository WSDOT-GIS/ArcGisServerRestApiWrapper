namespace Esri.ArcGisServer.Rest.Route
{
    /// <summary>
    /// <para>Use this parameter to specify additional values required by a restriction or to specify 
    /// whether the restriction prohibits, avoids, or prefers travel on roads that use the restriction. 
    /// If the restriction is meant to avoid or prefer roads, you can further specify the degree to which 
    /// they are avoided or preferred using this parameter.</para>
    /// <para>The parameter value is specified as an array of objects each having the attributeName,
    /// parameterName and value properties. The attributeName indicates the name of the restriction. 
    /// The parameterName indicates the name of the parameter associated with the restriction. 
    /// A restriction can have one or more parameterName properties based on its intended use. 
    /// The value indicates the value for a particular parameterName and is used by the service
    /// when evaluating the restriction. </para>
    /// </summary>
    /// <seealso href="http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/"/>
    public class AttributeParameterValue
    {
        /// <summary>The name of the attribute.</summary>
        public string attributeName { get; set; }
        /// <summary>
        /// The name of the parameter associated with the attribute.
        /// </summary>
        public string parameterName { get; set; }

        /// <summary>
        /// The value of the parameter. The type of object will vary depending on the attributeName and parameterName.
        /// </summary>
        public object value { get; set; }

        /////// <summary>Travel on the roads using the restriction is completely prohibited</summary>
        ////public const float Prohibited = -1;
        /////// <summary>It is very unlikely for the service to include in the route the roads that are associated with the restriction</summary>
        ////public const float Avoid_High = 5;
        /////// <summary>It is unlikely for the service to include in the route the roads that are associated with the restriction</summary>
        ////public const float Avoid_Medium = 2;
        /////// <summary>It is somewhat unlikely for the service to include in the route the roads that are associated with the restriction</summary>
        ////public const float Avoid_Low = 1.3F;
        /////// <summary>It is somewhat likely for the service to include in the route the roads that are associated with the restriction</summary>
        ////public const float Prefer_Low = 0.8F;
        /////// <summary>It is likely for the service to include in the route the roads that are associated with the restriction</summary>
        ////public const float Prefer_Medium = 0.5F;
        /////// <summary>It is very likely for the service to include in the route the roads that are associated with the restriction</summary>
        ////public const float Prefer_High = 0.2F;
    }
}
