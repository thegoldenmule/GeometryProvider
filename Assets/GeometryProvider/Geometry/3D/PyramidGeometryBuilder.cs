using System.ComponentModel;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Builds a pyramid.
    /// </summary>
    [DisplayName("Pyramid")]
    [Description("Builds a pyramid. Defaults to a tetrahedron.")]
    public class PyramidGeometryBuilder : CircleGeometryBuilder
    {
        /// <summary>
        /// Settings for building a pyramid.
        /// </summary>
        private PyramidGeometryBuilderSettings _settings;

        /// <summary>
        /// Number of sides on the pyramid.
        /// </summary>
        private int _numSides;

        /// <summary>
        /// Starts the build flow.
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="settings"></param>
        public override void Start(Mesh mesh, GeometryBuilderSettings settings)
        {
            _settings = (PyramidGeometryBuilderSettings) settings;

            _numSides = _settings.NumSides;
            CalculateBufferLength(_settings.NumSides, out _numVertices, out _numTriangles);

            _numVertices += 1;
        }

        /// <summary>
        /// Generates vertex and triangle layout.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="triangles"></param>
        public override void Layout(out Vector3[] vertices, out int[] triangles)
        {
            vertices = new Vector3[_numVertices];
            triangles = new int[(_numTriangles + _numSides) * 3];

            BuildCircle(
                _numSides,
                ref vertices,
                ref triangles);

            // add last vert
            vertices[_numVertices - 1] = new Vector3(0f, _settings.Height, 0f);

            // construct triangles to new vert
            var startTriangleIndex = _numSides * 3;
            for (int i = 0, len = _numSides; i < len; i++)
            {
                var triangleIndex = startTriangleIndex + i * 3;
                var vertIndex = i + 1;

                triangles[triangleIndex] = vertIndex;
                triangles[triangleIndex + 1] = _numVertices - 1;

                var index = vertIndex + 1;
                if (index >= _numVertices - 1)
                {
                    index = 1;
                }

                triangles[triangleIndex + 2] = index;
            }
        }

        /// <summary>
        /// Generates UVs.
        /// </summary>
        /// <param name="uvs"></param>
        /// <param name="settings"></param>
        public override void UV(
            out Vector2[] uvs,
            GeometryBuilderUVSettings settings)
        {
            uvs = new Vector2[_numVertices];

            BuildCircleUVs(
                _numSides,
                ref uvs);

            uvs[_numVertices - 1] = new Vector2(0.5f, 0.5f);
        }

        /// <summary>
        /// Custom factory.
        /// </summary>
        /// <returns></returns>
        [CustomFactory(typeof(PyramidGeometryBuilder))]
        private static GeometryBuilderSettings Settings()
        {
            return new PyramidGeometryBuilderSettings();
        }
    }
}