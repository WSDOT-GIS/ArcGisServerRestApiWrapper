
namespace Esri.ArcGisServer.Rest.Route
{
    public class AttributeParameterValue
    {
        public string attributeName { get; set; }
        public string parameterName { get; set; }
        /// <summary>
        /// Use one of the constants below
        /// </summary>
        public float value { get; set; }

        /// <summary>Travel on the roads using the restriction is completely prohibited</summary>
        public const float Prohibited = -1;
        /// <summary>It is very unlikely for the service to include in the route the roads that are associated with the restriction</summary>
        public const float Avoid_High = 5;
        /// <summary>It is unlikely for the service to include in the route the roads that are associated with the restriction</summary>
        public const float Avoid_Medium = 2;
        /// <summary>It is somewhat unlikely for the service to include in the route the roads that are associated with the restriction</summary>
        public const float Avoid_Low = 1.3F;
        /// <summary>It is somewhat likely for the service to include in the route the roads that are associated with the restriction</summary>
        public const float Prefer_Low = 0.8F;
        /// <summary>It is likely for the service to include in the route the roads that are associated with the restriction</summary>
        public const float Prefer_Medium = 0.5F;
        /// <summary>It is very likely for the service to include in the route the roads that are associated with the restriction</summary>
        public const float Prefer_High = 0.2F;
    }
}
