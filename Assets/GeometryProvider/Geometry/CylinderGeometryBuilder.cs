using System.ComponentModel;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    [DisplayName("Cylinder")]
    [Description("Builds a cylinder with the desired number of sides.")]
    public class CylinderGeometryBuilder : StandardGeometryBuilder
    {
        public override bool Build(Mesh mesh, GeometryBuilderSettings settings, out string error)
        {
            error = string.Empty;

            var cylinderSettings = (CylinderGeometryBuilderSettings) settings;
            /*
            var numSides = Mathf.Max(3, cylinderSettings.NumSides);

            // calculate buffer lengths by using circle builder
            int numVerts, numTriangles;
            CircleGeometryBuilder.CalculateBufferLength(
                numSides,
                out numVerts,
                out numTriangles);

            // top and bottom of cylinder, so multiply by two
            numVerts *= 2;
            numTriangles *= 2;

            // the user may not want the ends filled in
            if (!cylinderSettings.Endcaps)
            {
                numTriangles = 0;
            }

            // a quad for each side
            numTriangles += 2 * numSides;

            var vertices = new Vector3[numVerts];
            var triangles = new int[numTriangles * 3];

            // bottom
            

            Transform(ref vertices, settings);
            mesh.Apply(ref vertices, ref triangles);
            */
            return true;
        }

        [CustomFactory(typeof(CylinderGeometryBuilder))]
        private static GeometryBuilderSettings Factory()
        {
            return new CylinderGeometryBuilderSettings();
        }
    }
}