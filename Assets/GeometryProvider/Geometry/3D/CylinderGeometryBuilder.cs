using System.ComponentModel;
using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Constructos geometry for a cylinder with a variable number of sides.
    /// </summary>
    [DisplayName("Cylinder")]
    [Description("Builds a cylinder with the desired number of sides.")]
    public class CylinderGeometryBuilder : StandardGeometryBuilder
    {
        /// <summary>
        /// Settings for cylinder.
        /// </summary>
        private CylinderGeometryBuilderSettings _settings;

        /// <summary>
        /// Number of sides in a circle.
        /// </summary>
        private int _numSides;

        /// <summary>
        /// Various lookups into vert buffer.
        /// </summary>
        private int _startVertSideCircleTop = 0;
        private int _startVertSideCircleBottom = 0;
        private int _startVertCircleTop = 0;
        private int _startVertCircleBottom = 0;
        private int _startTriangleCircleTop = 0;
        private int _startTriangleCircleBottom = 0;

        /// <summary>
        /// Builds mesh.
        /// </summary>
        public override void Start(Mesh mesh, GeometryBuilderSettings settings)
        {
            _settings = (CylinderGeometryBuilderSettings) settings;

            _numSides = Mathf.Max(3, _settings.NumSides);

            // calculate buffer lengths

            // start with sides
            int numTotalVerts = 2 * _numSides;
            int numTotalTriangles = 2 * _numSides;

            // add endcaps
            if (_settings.Endcaps)
            {
                numTotalVerts += 2 * (_numSides + 1);
                numTotalTriangles += 2 * _numSides;
            }

            _numVertices = numTotalVerts;
            _numTriangles = numTotalTriangles;

            _startVertSideCircleTop = 0;
            _startVertSideCircleBottom = _numSides;
            _startVertCircleTop = 2 * _numSides;
            _startVertCircleBottom = _startVertCircleTop + _numSides + 1;

            _startTriangleCircleTop = _numSides * 6;
            _startTriangleCircleBottom = _startTriangleCircleBottom + 3 * _numSides;

            Debug.Log("NumSides : " + _numSides);
            Debug.Log("NumVerts : " + _numVertices);
            Debug.Log("NumTris  : " + _numTriangles);
            Debug.Log("StartVertSideCircleTop : " + _startVertSideCircleTop);
            Debug.Log("StartVertSideCircleBottom : " + _startVertSideCircleBottom);
            Debug.Log("StartVertCircleTop : " + _startVertCircleTop);
            Debug.Log("StartVertCircleBottom : " + _startVertCircleBottom);
        }

        /// <summary>
        /// Provides layout for cylinder.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="triangles"></param>
        public override void Layout(out Vector3[] vertices, out int[] triangles)
        {
            var halfHeight = _settings.Height / 2f;

            vertices = new Vector3[_numVertices];
            triangles = new int[_numTriangles * 3];

            // sides
            {
                // make two circles back to back
                var radians = 2f * Mathf.PI / _numSides;
                for (var i = 0; i < _numSides; i++)
                {
                    var cosine = Mathf.Cos(radians * (i - 1));
                    var sine = Mathf.Sin(radians * (i - 1));

                    vertices[_startVertSideCircleBottom + i] = 0.5f * new Vector3(
                            cosine,
                            -halfHeight,
                            sine);
                    vertices[_startVertSideCircleTop + i] = 0.5f * new Vector3(
                            cosine,
                            halfHeight,
                            sine);
                }

                // now make the sides
                var topVertEnd = _startVertSideCircleTop + _numSides;
                var bottomVertEnd = _startVertSideCircleBottom + _numSides;
                for (int quad = 0, len = _numSides; quad < len; quad++)
                {
                    // get 4 corners
                    var topVertIndex = _startVertSideCircleTop + quad;
                    var bottomVertIndex = _startVertSideCircleBottom + quad;

                    var topVertNextIndex = topVertIndex + 1;
                    if (topVertNextIndex > topVertEnd)
                    {
                        topVertNextIndex = _startVertSideCircleTop;
                    }

                    var bottomVertNextIndex = bottomVertIndex + 1;
                    if (bottomVertNextIndex > bottomVertEnd)
                    {
                        bottomVertNextIndex = _startVertSideCircleBottom;
                    }

                    var triangleIndex = 6 * quad;

                    triangles[triangleIndex] = topVertIndex;
                    triangles[triangleIndex + 1] = bottomVertNextIndex;
                    triangles[triangleIndex + 2] = bottomVertIndex;

                    triangles[triangleIndex + 3] = topVertIndex;
                    triangles[triangleIndex + 4] = topVertNextIndex;
                    triangles[triangleIndex + 5] = bottomVertNextIndex;
                }
            }

            if (!_settings.Endcaps)
            {
                return;
            }

            // caps
            {
                var numCircleVerts = _numSides + 1;
                var numCircleTriangles = _numSides;

                // top
                BuildCap(
                    ref vertices,
                    ref triangles,
                    halfHeight,
                    _startVertCircleTop,
                    _startTriangleCircleTop);

                // bottom
                BuildCap(
                    ref vertices,
                    ref triangles,
                    -halfHeight,
                    _startVertCircleBottom,
                    _startTriangleCircleBottom);
            }
        }

        /// <summary>
        /// Provides uvs for cylinder.
        /// </summary>
        public override void UV(out Vector2[] uvs, GeometryBuilderUVSettings settings)
        {
            uvs = new Vector2[_numVertices];

            for (var i = 0; i < _numSides; i++)
            {
                var u = (float) i / _numSides;
                uvs[i] = new Vector2(u, 0);
                uvs[i + _numSides] = new Vector2(u, 1);
            }

            if (_settings.Endcaps)
            {
                BuildCircleUVs(2 * _numSides, ref uvs);
                BuildCircleUVs(3 * _numSides + 1, ref uvs);
            }
        }

        /// <summary>
        /// Builds uv buffer for a circle.
        /// </summary>
        private void BuildCircleUVs(
            int startIndex,
            ref Vector2[] uvs)
        {
            uvs[startIndex] = new Vector2(0.5f, 0.5f);

            var radians = 2f * Mathf.PI / _numSides;
            for (var i = 1; i <= _numSides; i++)
            {
                var cosine = Mathf.Cos(radians * (i - 1));
                var sine = Mathf.Sin(radians * (i - 1));

                uvs[i] = 0.5f * (Vector2.one + new Vector2(cosine, sine));
            }
        }

        /// <summary>
        /// Builds an endcap.
        /// </summary>
        private void BuildCap(
            ref Vector3[] vertices,
            ref int[] triangles,
            float y,
            int startVertIndex,
            int startTriangleIndex)
        {
            var numCircleVerts = _numSides + 1;
            var numCircleTriangles = _numSides;

            vertices[startVertIndex] = new Vector3(0f, y, 0f);

            var radians = 2f * Mathf.PI / _numSides;
            for (var i = 1; i < numCircleVerts; i++)
            {
                vertices[startVertIndex + i] = new Vector3(
                    0.5f * Mathf.Cos(radians * (i - 1)),
                    y,
                    0.5f * Mathf.Sin(radians * (i - 1)));
            }

            for (var i = 0; i < numCircleTriangles * 3; i += 3)
            {
                var vertIndex = i / 3 + 1;

                triangles[startTriangleIndex + i] = startVertIndex + vertIndex;
                triangles[startTriangleIndex + i + 2] = startVertIndex;

                vertIndex = vertIndex + 1;
                if (vertIndex >= numCircleVerts)
                {
                    vertIndex = 1;
                }

                triangles[startTriangleIndex + i + 1] = startVertIndex + vertIndex;
            }
        }

        /// <summary>
        /// Custom factory for cylinder settings.
        /// </summary>
        /// <returns></returns>
        [CustomFactory(typeof(CylinderGeometryBuilder))]
        private static GeometryBuilderSettings Factory()
        {
            return new CylinderGeometryBuilderSettings();
        }
    }
}