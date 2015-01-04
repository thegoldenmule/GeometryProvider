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
        /// If true, the generated polygon is convex. If false, the generated
        /// polygon is a star polygon.
        /// </summary>
        public bool Convex = true;

        /// <summary>
        /// The number of sides.
        /// </summary>
        public int NumSides = 3;
    }
}
