
namespace Esri.ArcGisServer.Rest.Geocode
{
    /// <summary>
    /// Specifies if the output geometry of PointAddress matches should be the rooftop point or street entrance location. Valid values are rooftop and street. The default value is street.
    /// </summary>
    /// <remarks>
    /// Geocode results include one geometry object (the location object) which defines the location of the address, as well as two sets of X/Y coordinate values within the attributes object: X/Y, and DisplayX/DisplayY. 
    /// For geocode results with Addr_type=PointAddress, the X/Y attribute values describe the coordinates of the address along the street, while the DisplayX/DisplayY values describe the rooftop, or building centroid, coordinates. 
    /// By default the geometry returned for geocode results represents the street entrance location of the address. This is useful for routing scenarios because the rooftop location of some addresses may be offset from a street by a large distance. 
    /// However for map display purposes it may be desirable to show the rooftop location instead, especially when large buildings or landmarks are geocoded. For these cases the locationType parameter can be used to specify that the rooftop geometry should be returned.
    /// <para>Note: The locationType parameter only affects the location object in the geocode JSON response. It does not change the X/Y or DisplayX/DisplayY attribute values.</para>
    /// </remarks>
    public enum LocationType
    {
        ///<summary>Coordinates in location object in response refer to rooftop or building centroid.</summary>
        rooftop,
        ///<summary>Coordinates in location object in response refer to street entrance location for address.</summary>
        street
    }
}
