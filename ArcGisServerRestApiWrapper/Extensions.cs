using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esri.ArcGisServer.Rest
{
    /// <summary>
    /// Provides extension methods.
    /// </summary>
    public static class Extensions
    {
        // PowerShell: ([DateTime]"Jan 1, 1970") + [TimeSpan](1199145600000 * 10000) = (1 Jan 2008 00:00:00 GMT) 

        /* $d = [DateTime]"Jan 1, 2008"
         * $baseTime = [DateTime]"Jan 1, 1970"
         * ($d - $baseTime).Ticks / 10000
         */

        private static readonly DateTime _jsBaseTime = new DateTime(1970, 1, 1);

        /// <summary>
        /// Converts a <see cref="DateTime"/> value into the equivalent "timeInstant" value expected by the REST API.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>(<paramref name="dateTime"/> - 1970-01-01T00:00).<see cref="TimeSpan.Ticks">Ticks</see> / 10000</returns>
        public static long ToJavaScriptTicks(this DateTime dateTime)
        {
            return (dateTime - _jsBaseTime).Ticks / 10000;
        }

        /// <summary>
        /// Converts a double[][] into a list of points. Coordinates in the point are separated by commas (,) , points are separated by a semicolon (;).
        /// </summary>
        /// <typeparam name="T">An enumeration of <see cref="double"/></typeparam>
        /// <param name="points">A jagged array of doubles.</param>
        /// <returns></returns>
        public static string ToListString<T>(this IEnumerable<T> points) where T: IEnumerable<double>
        {
            return string.Join(";", from coordinates in points 
                                    select string.Join(",", coordinates));
        }

        /// <summary>
        /// Adds the first non-<see langword="null"/> value from <paramref name="values"/> to <paramref name="dict"/>.
        /// If all objects in <paramref name="values"/> are <see langword="null"/> then nothing is added to <paramref name="dict"/>.
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns>Returns <see langword="true"/> if an item was added, <see langword="false"/> if not.</returns>
        public static bool AddFirstNonNullValue(this IDictionary<string, object> dict, string key, params object[] values)
        {
            var val = values.FirstOrDefault(p => p != null);
            bool output = false;
            if (val != null)
            {
                dict.Add(key, val);
                output = true;
            }
            return output;
        }

        /// <summary>
        /// Converts a dictionary into a query string.
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string ToQueryString(this IDictionary<string, object> dict)
        {
            string fmt = "{0}={1}";

            var output = string.Join("&", 
                from kvp in dict
                select string.Format(fmt, kvp.Key, ConvertToValueForQueryString(kvp.Value))
                );

            return output;

        }

        /// <summary>
        /// Converts a value into a string suitable for use as a query string value. The string
        /// is run through <see cref="HttpUtility.UrlEncode(string)"/> before being returned.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToValueForQueryString(object value)
        {
            Type t = value.GetType();
            TypeCode tc = Type.GetTypeCode(t);
            string output;

            switch (tc)
            {
                case TypeCode.Object:
                    output = HttpUtility.UrlEncode(JsonConvert.SerializeObject(value));
                    break;
                case TypeCode.String:
                    output = HttpUtility.UrlEncode((string)value);
                    break;
                default:
                    output = value.ToString();
                    break;
            }

            return output;

            
        }
    }
}
