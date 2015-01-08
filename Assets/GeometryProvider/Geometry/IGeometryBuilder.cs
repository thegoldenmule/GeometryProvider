using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Describes an object that can create geometry.
    /// </summary>
    public interface IGeometryBuilder
    {
        /// <summary>
        /// Called one time when the editor is started.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Called to start the geometry build flow.
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="settings"></param>
        void Start(Mesh mesh, GeometryBuilderSettings settings);

        /// <summary>
        /// Retrieves the vertex + triangle layout.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="triangles"></param>
        void Layout(
            out Vector3[] vertices,
            out int[] triangles);

        /// <summary>
        /// Retrieves UVs.
        /// </summary>
        /// <param name="uvs"></param>
        /// <param name="settings"></param>
        void UV(
            out Vector2[] uvs,
            GeometryBuilderUVSettings settings);

        /// <summary>
        /// Retrieves normals.
        /// </summary>
        /// <param name="normals"></param>
        void Normals(out Vector3[] normals);

        /// <summary>
        /// Retrieves colors.
        /// </summary>
        /// <param name="colors"></param>
        void Colors(out Color[] colors);
    }
}