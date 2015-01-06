using UnityEngine;

namespace TheGoldenMule.Geo
{
    public abstract class StandardGeometryBuilder : IGeometryBuilder
    {
        public virtual void Initialize()
        {
            // 
        }

        public virtual void Build(
            Mesh mesh,
            GeometryBuilderSettings settings)
        {
            
        }

        public virtual void ApplyAllDefaults(
            Mesh mesh,
            GeometryBuilderSettings settings)
        {
            settings.UV.ApplyDefault(mesh);
            settings.UV2.ApplyDefault(mesh);
            settings.Color.ApplyDefault(mesh);
            settings.Tangents.ApplyDefault(mesh);
            settings.Normals.ApplyDefault(mesh);
        }
    }
}