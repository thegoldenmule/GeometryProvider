using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheGoldenMule.Geo.Editor
{
    public interface IGeometryBuilderRenderer
    {
        void Draw(GeometryBuilderSettings settings);
    }
}