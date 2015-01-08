using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Base implementation of IGeometryBuilder that returns null meshes.
    /// </summary>
    public class StandardGeometryBuilder : IGeometryBuilder
    {
        /// <summary>
        /// Number of verts!
        /// </summary>
        protected int _numVertices;

        /// <summary>
        /// Number of triangles
        /// </summary>
        protected int _numTriangles;

        /// <summary>
        /// Initializes the builder.
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// Starts the build flow.
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="settings"></param>
        public virtual void Start(Mesh mesh, GeometryBuilderSettings settings)
        {
            _numVertices = _numTriangles = -1;
        }

        /// <summary>
        /// Retrurns a null layout.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="triangles"></param>
        public virtual void Layout(out Vector3[] vertices, out int[] triangles)
        {
            vertices = null;
            triangles = null;
        }

        /// <summary>
        /// Returns null UVs.
        /// </summary>
        /// <param name="uvs"></param>
        /// <param name="settings"></param>
        public virtual void UV(out Vector2[] uvs, GeometryBuilderUVSettings settings)
        {
            uvs = null;
        }

        /// <summary>
        /// Returns null normals.
        /// </summary>
        /// <param name="normals"></param>
        public virtual void Normals(out Vector3[] normals)
        {
            if (-1 == _numVertices)
            {
                normals = null;

                return;
            }
            
            normals = new Vector3[_numVertices].Fill(new Vector3(0, 1, 0));
        }

        /// <summary>
        /// Returns null colors.
        /// </summary>
        /// <param name="colors"></param>
        public virtual void Colors(out Color[] colors)
        {
            if (-1 == _numVertices)
            {
                colors = null;

                return;
            }

            colors = new Color[_numVertices].Fill(Color.white);
        }
    }
}
