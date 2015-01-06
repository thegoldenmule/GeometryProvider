using UnityEngine;

namespace TheGoldenMule.Geo
{
    public interface IGeometryBuilder
    {
        void Initialize();
        void Build(Mesh mesh, GeometryBuilderSettings settings);
    }
}