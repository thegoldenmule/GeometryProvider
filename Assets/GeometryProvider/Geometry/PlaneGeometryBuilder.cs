using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    [DisplayName("Plane")]
    [Description("Builds a plane in XZ space.")]
    public class PlaneGeometryBuilder : StandardGeometryBuilder
    {
        public override bool Build(
            Mesh mesh,
            GeometryBuilderSettings settings,
            out string error)
        {
            error = string.Empty;

            var planeSettings = (PlaneGeometryBuilderSettings) settings;

            BuildCompactPlane(mesh, planeSettings);

            if (!settings.ShareVerts)
            {
                UnshareVerts(mesh);
            }

            return true;
        }

        private static void BuildCompactPlane(Mesh mesh, PlaneGeometryBuilderSettings settings)
        {
            var xVerts = Mathf.Max(2, settings.NumXVerts);
            var zVerts = Mathf.Max(2, settings.NumZVerts);
            
            var numQuads = (xVerts - 1) * (zVerts - 1);
            var numVerts = xVerts * zVerts;
            var numIndices = numQuads * 6;

            var vertices = new Vector3[numVerts];
            var indices = new int[numIndices];

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

                    indices[index] = x + z * xVerts;
                    indices[index + 1] = x + 1 + (z + 1) * xVerts;
                    indices[index + 2] = x + 1 + z * xVerts;

                    indices[index + 3] = x + z * xVerts;
                    indices[index + 4] = x + (z + 1) * xVerts;
                    indices[index + 5] = x + 1 + (z + 1) * xVerts;
                }
            }

            Transform(ref vertices, settings);

            mesh.Apply(ref vertices, ref indices);
        }

        [CustomFactory(typeof(PlaneGeometryBuilder))]
        private static GeometryBuilderSettings Settings()
        {
            return new PlaneGeometryBuilderSettings();
        }
    }
}
