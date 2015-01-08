using System.ComponentModel;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Creates a sphere using Unity's PrimitiveType.
    /// </summary>
    [DisplayName("Unity Sphere")]
    [Description("Creates a sphere using Unity's builtin method.")]
    public class UnitySphereGeometryBuilder : StandardGeometryBuilder
    {
        /// <summary>
        /// Pulled off of the Unity sphere.
        /// </summary>
        private Vector3[] _vertices;
        private Vector2[] _uvs;
        private int[] _triangles;
        private Vector3[] _normals;

        /// <summary>
        /// On start, creates a Unity sphere and pulls off buffers.
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="settings"></param>
        public override void Start(Mesh mesh, GeometryBuilderSettings settings)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.hideFlags = HideFlags.HideAndDontSave;

            var sphereMesh = sphere.GetComponent<MeshFilter>().sharedMesh;

            _vertices = sphereMesh.vertices;
            _triangles = sphereMesh.triangles;
            _uvs = sphereMesh.uv;
            _normals = sphereMesh.normals;

            if (Application.isPlaying)
            {
                Object.Destroy(sphere);
            }
            else
            {
                Object.DestroyImmediate(sphere);
            }
        }

        /// <summary>
        /// Returns vertex + triangle layout.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="triangles"></param>
        public override void Layout(out Vector3[] vertices, out int[] triangles)
        {
            vertices = _vertices;
            triangles = _triangles;
        }

        /// <summary>
        /// Returns uvs.
        /// </summary>
        /// <param name="uvs"></param>
        /// <param name="settings"></param>
        public override void UV(out Vector2[] uvs, GeometryBuilderUVSettings settings)
        {
            uvs = _uvs;
        }

        /// <summary>
        /// Returns normals.
        /// </summary>
        /// <param name="normals"></param>
        public override void Normals(out Vector3[] normals)
        {
            normals = _normals;
        }
    }
}
