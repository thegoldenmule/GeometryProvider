using System.ComponentModel;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Builds geometry for a circle.
    /// </summary>
    [DisplayName("Circle")]
    [Description("Builds a circle with the desired number of edges.")]
    public class CircleGeometryBuilder : StandardGeometryBuilder
    {
        /// <summary>
        /// Settings.
        /// </summary>
        private CircleGeometryBuilderSettings _settings;

        /// <summary>
        /// Number of sides.
        /// </summary>
        private int _numSides;

        /// <summary>
        /// Builds geometry for a circle.
        /// </summary>
        public override void Start(Mesh mesh, GeometryBuilderSettings settings)
        {
            base.Start(mesh, settings);

            _settings = (CircleGeometryBuilderSettings) settings;
            _numSides = Mathf.Max(3, _settings.NumSides);

            CalculateBufferLength(_numSides, out _numVertices, out _numTriangles);
        }

        /// <summary>
        /// Returns circle geo.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="triangles"></param>
        public override void Layout(out Vector3[] vertices, out int[] triangles)
        {
            var numIndices = _numTriangles * 3;

            vertices = new Vector3[_numVertices];
            triangles = new int[numIndices];

            BuildCircle(
                _numSides,
                ref vertices,
                ref triangles);
        }

        /// <summary>
        /// Builds circle UVs.
        /// </summary>
        public override void UV(out Vector2[] uvs, GeometryBuilderUVSettings settings)
        {
            uvs = new Vector2[_numVertices];

            BuildCircleUVs(
                _numSides,
                ref uvs);
        }

        /// <summary>
        /// Calculates the number of verts and triangles needed.
        /// </summary>
        protected void CalculateBufferLength(
            int numSides,
            out int numVerts,
            out int numTriangles)
        {
            numVerts = numSides + 1;
            numTriangles = numSides;
        }

        /// <summary>
        /// Builds uv buffer.
        /// </summary>
        /// <param name="numSides"></param>
        /// <param name="uvs"></param>
        protected void BuildCircleUVs(
            int numSides,
            ref Vector2[] uvs)
        {
            int numVerts, numTriangles;
            CalculateBufferLength(numSides, out numVerts, out numTriangles);

            uvs[0] = new Vector2(0.5f, 0.5f);

            var radians = 2f * Mathf.PI / numSides;
            for (var i = 1; i < numVerts; i++)
            {
                var cosine = Mathf.Cos(radians * (i - 1));
                var sine = Mathf.Sin(radians * (i - 1));

                uvs[i] = 0.5f * (Vector2.one + new Vector2(cosine, sine));
            }
        }

        /// <summary>
        /// Actually builds the vertex and index buffers.
        /// </summary>
        protected void BuildCircle(
            int numSides,
            ref Vector3[] vertices,
            ref int[] triangles)
        {
            int numVerts, numTriangles;
            CalculateBufferLength(numSides, out numVerts, out numTriangles);

            vertices[0] = Vector3.zero;

            var radians = 2f * Mathf.PI / numSides;
            for (var i = 1; i < numVerts; i++)
            {
                var cosine = Mathf.Cos(radians * (i - 1));
                var sine = Mathf.Sin(radians * (i - 1));
                
                vertices[i] = 0.5f * new Vector3(
                        cosine,
                        0f,
                        sine);
            }

            for (var i = 0; i < numTriangles * 3; i += 3)
            {
                var vertIndex = i / 3 + 1;

                triangles[i] = vertIndex;
                triangles[i + 1] = 0;

                vertIndex = vertIndex + 1;
                if (vertIndex >= numVerts)
                {
                    vertIndex = 1;
                }

                triangles[i + 2] = vertIndex;
            }
        }

        /// <summary>
        /// Custom factory method for CircleGeometry settings.
        /// </summary>
        /// <returns></returns>
        [CustomFactory(typeof(CircleGeometryBuilder))]
        private static GeometryBuilderSettings Factory()
        {
            return new CircleGeometryBuilderSettings();
        }
    }
}
