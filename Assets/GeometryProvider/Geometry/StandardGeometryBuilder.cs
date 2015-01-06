using UnityEngine;

namespace TheGoldenMule.Geo
{
    public abstract class StandardGeometryBuilder : IGeometryBuilder
    {
        public virtual void Initialize()
        {
            // 
        }

        public virtual void Build(Mesh mesh, GeometryBuilderSettings settings)
        {
            
        }

        protected virtual void UnshareVerts(Mesh mesh)
        {

        }

        protected static void Transform(
            ref Vector3[] vertices,
            GeometryBuilderSettings settings)
        {
            var transformation = settings.Transform.TRS();
            for (int i = 0, len = vertices.Length; i < len; i++)
            {
                vertices[i] = transformation.MultiplyPoint(vertices[i]);
            }
        }
    }
}
