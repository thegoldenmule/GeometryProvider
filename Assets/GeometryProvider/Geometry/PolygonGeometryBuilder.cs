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
            var indices = new int[numIndices];

            vertices[0] = Vector3.zero;

            var radians = 2f * Mathf.PI / numSides;
            for (var i = 1; i < numVerts; i++)
            {
                vertices[i] = 0.5f * new Vector3(
                    Mathf.Cos(radians * (i - 1)),
                    0f,
                    Mathf.Sin(radians * (i - 1)));
            }

            for (var i = 0; i < numTriangles; i++)
            {
                var triangleIndex = i * 3;
                var vertIndex = i + 1;

                indices[triangleIndex] = vertIndex;
                indices[triangleIndex + 1] = 0;
                indices[triangleIndex + 2] = Mathf.Max(1, (vertIndex + 1) % numVerts);
            }

            Transform(ref vertices, settings);

            mesh.Apply(ref vertices, ref indices);

            return true;
        }

        [CustomFactory(typeof(PolygonGeometryBuilder))]
        private static GeometryBuilderSettings Factory()
        {
            return new PolygonGeometryBuilderSettings();
        }
    }
}
