using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcGisServerRestApiWrapper;
using System.Diagnostics;
using System.IO;

namespace WrapperTest
{
	class Program
	{
		static void Main(string[] args)
		{
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
			var mapService = new MapService { Uri = new Uri("http://wsdot.wa.gov/geosvcs/ArcGIS/rest/services/Shared/WebBaseMapWebMercator/MapServer") };
			using (var stream = mapService.ExportMap(parameters))
			{
				using (FileStream fs = new FileStream("output.png", FileMode.Create))
				{
					stream.CopyTo(fs);
				}
			}
		}
	}
}
