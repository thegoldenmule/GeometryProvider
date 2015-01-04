using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Settings for generating a convex or star polygon.
    /// </summary>
    public class PolygonGeometryBuilderSettings : GeometryBuilderSettings
    {
        /// <summary>
        /// The number of sides.
        /// </summary>
        public int NumSides = 3;
    }
}
