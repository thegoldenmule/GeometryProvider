using System.ComponentModel;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Creates a sphere using Unity's PrimitiveType.
    /// </summary>
    [DisplayName("Unity Sphere")]
    [Description("Creates a sphere using Unity's builtin method.")]
    public class UnitySphereGeometryBuilder : StandardGeometryBuilder
    {
        public override void Build(Mesh mesh, GeometryBuilderSettings settings)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            var sphereMesh = sphere.GetComponent<MeshFilter>().mesh;

            var vertices = sphereMesh.vertices;
            var triangles = sphereMesh.triangles;

            Transform(ref vertices, settings);
            mesh.Apply(ref vertices, ref triangles);

            if (Application.isPlaying)
            {
                Object.Destroy(sphere);
            }
            else
            {
                Object.DestroyImmediate(sphere);
            }
        }
    }
}
