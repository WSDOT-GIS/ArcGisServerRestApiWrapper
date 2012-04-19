using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcGisServerRestApiWrapper
{
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
	}
}
