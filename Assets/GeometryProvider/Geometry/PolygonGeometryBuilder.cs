using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    [DisplayName("Polygon")]
    [Description("Builds any regular polygon.")]
    public class PolygonGeometryBuilder : StandardGeometryBuilder
    {
        public override bool Build(Mesh mesh, GeometryBuilderSettings settings, out string error)
        {
            error = string.Empty;

            var polygonSettings = (PolygonGeometryBuilderSettings) settings;

            var numSides = Mathf.Max(3, polygonSettings.NumSides);
            var numVerts = numSides + 1;
            var numTriangles = numSides;
            var numIndices = numTriangles * 3;

            var vertices = new Vector3[numVerts];
            var triangles = new int[numIndices];

            BuildPolygon(
                numSides,
                true,
                0, 0,
                ref vertices,
                ref triangles);

            Transform(ref vertices, settings);

            mesh.Apply(ref vertices, ref triangles);

            return true;
        }

        public static void BuildPolygon(
            int numSides,
            bool fill,
            int startVertexIndex,
            int startTriangleIndex,
            ref Vector3[] vertices,
            ref int[] triangles)
        {
            var numVerts = numSides + 1;
            vertices[startVertexIndex] = Vector3.zero;

            var radians = 2f * Mathf.PI / numSides;
            for (var i = startVertexIndex + 1; i < startVertexIndex + numVerts; i++)
            {
                vertices[i] = 0.5f * new Vector3(
                    Mathf.Cos(radians * (i - startVertexIndex - 1)),
                    0f,
                    Mathf.Sin(radians * (i - startVertexIndex - 1)));
            }

            if (fill)
            {
                var numTriangles = numSides;
                for (var i = startTriangleIndex; i < startTriangleIndex + numTriangles; i++)
                {
                    var triangleIndex = (i - startTriangleIndex) * 3;
                    var vertIndex = (i - startTriangleIndex) + 1;

                    triangles[triangleIndex] = vertIndex;
                    triangles[triangleIndex + 1] = startVertexIndex;
                    triangles[triangleIndex + 2] = Mathf.Max(1, (vertIndex + 1) % numVerts);
                }
            }
        }

        [CustomFactory(typeof(PolygonGeometryBuilder))]
        private static GeometryBuilderSettings Factory()
        {
            return new PolygonGeometryBuilderSettings();
        }
    }
}
