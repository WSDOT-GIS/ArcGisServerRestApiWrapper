using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.ArcGisServer.Rest
{
    public interface IHasZ
    {
        bool hasZ { get; set; }
    }

    public interface IHasM
    {
        bool hasM { get; set; }
    }

    public interface IHasZM : IHasZ, IHasM
    {

    }
}
