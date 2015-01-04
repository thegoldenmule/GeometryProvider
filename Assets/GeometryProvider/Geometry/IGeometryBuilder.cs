using UnityEngine;
using System.Collections;

namespace TheGoldenMule.Geo
{
    public interface IGeometryBuilder
    {
        void Initialize();
        bool Build(Mesh mesh, GeometryBuilderSettings settings, out string error);
    }
}