using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheGoldenMule.Geo
{
    [Serializable]
    public class PlaneGeometryBuilderSettings : GeometryBuilderSettings
    {
        /// <summary>
        /// The number of verts along the x axis.
        /// </summary>
        public int NumXVerts;

        /// <summary>
        /// The number of verts along the z axis.
        /// </summary>
        public int NumZVerts;
    }
}
