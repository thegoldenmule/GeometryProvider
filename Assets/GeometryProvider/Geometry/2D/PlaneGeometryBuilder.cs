using System.ComponentModel;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Builds geometry for a plane.
    /// </summary>
    [DisplayName("Plane")]
    [Description("Builds a plane in XZ space.")]
    public class PlaneGeometryBuilder : StandardGeometryBuilder
    {
        /// <summary>
        /// Custom plane settings.
        /// </summary>
        private PlaneGeometryBuilderSettings _settings;

        /// <summary>
        /// Numver of verts in the x direction.
        /// </summary>
        private int _xVerts;

        /// <summary>
        /// Number of verts in the z direction.
        /// </summary>
        private int _zVerts;

        /// <summary>
        /// Total number of quads.
        /// </summary>
        private int _numQuads;

        /// <summary>
        /// Starts the contruction flow.
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="settings"></param>
        public override void Start(
            Mesh mesh,
            GeometryBuilderSettings settings)
        {
            _settings = (PlaneGeometryBuilderSettings) settings;

            _xVerts = Mathf.Max(2, _settings.NumXVerts);
            _zVerts = Mathf.Max(2, _settings.NumZVerts);
            
            _numQuads = (_xVerts - 1) * (_zVerts - 1);
            _numVertices = _xVerts * _zVerts;
            _numTriangles = _numQuads * 2;
        }

        /// <summary>
        /// Creates vert + triangle layout for a plane.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="triangles"></param>
        public override void Layout(
            out Vector3[] vertices,
            out int[] triangles)
        {
            var numIndices = _numQuads * 6;

            vertices = new Vector3[_numVertices];
            triangles = new int[numIndices];

            var invXVerts = 1f / (_xVerts - 1);
            var invZVerts = 1f / (_zVerts - 1);

            for (int x = 0; x < _xVerts; x++)
            {
                for (int z = 0; z < _zVerts; z++)
                {
                    int vertIndex = x + z * _xVerts;

                    vertices[vertIndex] = new Vector3(
                        x * invXVerts - 0.5f,
                        0,
                        z * invZVerts - 0.5f);
                }
            }

            for (int x = 0; x < _xVerts - 1; x++)
            {
                for (int z = 0; z < _zVerts - 1; z++)
                {
                    var quadIndex = x + z * (_xVerts - 1);
                    var index = quadIndex * 6;

                    triangles[index] = x + z * _xVerts;
                    triangles[index + 1] = x + 1 + (z + 1) * _xVerts;
                    triangles[index + 2] = x + 1 + z * _xVerts;

                    triangles[index + 3] = x + z * _xVerts;
                    triangles[index + 4] = x + (z + 1) * _xVerts;
                    triangles[index + 5] = x + 1 + (z + 1) * _xVerts;
                }
            }
        }

        /// <summary>
        /// Generates UVs for a plane.
        /// </summary>
        /// <param name="uvs"></param>
        /// <param name="settings"></param>
        public override void UV(out Vector2[] uvs, GeometryBuilderUVSettings settings)
        {
            uvs = new Vector2[_numVertices];

            var invXVerts = 1f / (_xVerts - 1);
            var invZVerts = 1f / (_zVerts - 1);

            for (int x = 0; x < _xVerts; x++)
            {
                for (int z = 0; z < _zVerts; z++)
                {
                    int vertIndex = x + z * _xVerts;

                    uvs[vertIndex] = new Vector2(
                        x * invXVerts,
                        z * invZVerts);
                }
            }
        }

        /// <summary>
        /// Custom factory for plane settings.
        /// </summary>
        /// <returns></returns>
        [CustomFactory(typeof(PlaneGeometryBuilder))]
        private static GeometryBuilderSettings Settings()
        {
            return new PlaneGeometryBuilderSettings();
        }
    }
}
