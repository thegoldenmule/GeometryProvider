using System.ComponentModel;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    [DisplayName("Plane")]
    [Description("Builds a plane in XZ space.")]
    public class PlaneGeometryBuilder : StandardGeometryBuilder
    {
        public override void Build(
            Mesh mesh,
            GeometryBuilderSettings settings)
        {
            var planeSettings = (PlaneGeometryBuilderSettings) settings;

            BuildCompactPlane(mesh, planeSettings);
        }

        private void BuildCompactPlane(Mesh mesh, PlaneGeometryBuilderSettings settings)
        {
            var xVerts = Mathf.Max(2, settings.NumXVerts);
            var zVerts = Mathf.Max(2, settings.NumZVerts);
            
            var numQuads = (xVerts - 1) * (zVerts - 1);
            var numVerts = xVerts * zVerts;
            var numIndices = numQuads * 6;

            var vertices = new Vector3[numVerts];
            var triangles = new int[numIndices];

            var invXVerts = 1f / (xVerts - 1);
            var invZVerts = 1f / (zVerts - 1);
            
            for (int x = 0; x < xVerts; x++)
            {
                for (int z = 0; z < zVerts; z++)
                {
                    vertices[x + z * xVerts] = new Vector3(
                        x * invXVerts - 0.5f,
                        0,
                        z * invZVerts - 0.5f);
                }
            }

            for (int x = 0; x < xVerts - 1; x++)
            {
                for (int z = 0; z < zVerts - 1; z++)
                {
                    var quadIndex = x + z * (xVerts - 1);
                    var index = quadIndex * 6;

                    triangles[index] = x + z * xVerts;
                    triangles[index + 1] = x + 1 + (z + 1) * xVerts;
                    triangles[index + 2] = x + 1 + z * xVerts;

                    triangles[index + 3] = x + z * xVerts;
                    triangles[index + 4] = x + (z + 1) * xVerts;
                    triangles[index + 5] = x + 1 + (z + 1) * xVerts;
                }
            }

            settings.Vertex.ApplyDefault(mesh, ref vertices, ref triangles);

            ApplyAllDefaults(mesh, settings);
        }

        [CustomFactory(typeof(PlaneGeometryBuilder))]
        private static GeometryBuilderSettings Settings()
        {
            return new PlaneGeometryBuilderSettings();
        }
    }
}
