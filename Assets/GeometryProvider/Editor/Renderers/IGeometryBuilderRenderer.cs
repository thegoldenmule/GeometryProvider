using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheGoldenMule.Geo.Editor
{
    public interface IGeometryBuilderRenderer
    {
        event Action OnCreate;
        event Action OnUpdate;

        void Draw(GeometryBuilderSettings settings);
    }
}