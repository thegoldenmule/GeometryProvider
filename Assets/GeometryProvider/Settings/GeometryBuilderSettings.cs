using System;

namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Each buffer Unity supports.
    /// </summary>
    public enum Buffer
    {
        Vertex,
        UV,
        UV2,
        Color,
        Normal,
        Tangent
    }

    [Serializable]
    public class GeometryBuilderSettings
    {
        /// <summary>
        /// Descriptive name. This is used by the editor and will be null at
        /// runtime.
        /// </summary>
        [NonSerialized]
        public string Name;

        /// <summary>
        /// Description of builder. This is used by the editor and will be null
        /// at runtime.
        /// </summary>
        [NonSerialized]
        public string Description;

        /// <summary>
        /// If true, allows verts to be shared between triangles. If false,
        /// each triangle will have its own verts.
        /// </summary>
        public bool ShareVerts = true;

        /// <summary>
        /// Settings for vertices.
        /// </summary>
        public GeometryBuilderVertexSettings Vertex = new GeometryBuilderVertexSettings();

        /// <summary>
        /// Settings for UVs.
        /// </summary>
        public GeometryBuilderUVSettings UV = new GeometryBuilderUVSettings();

        /// <summary>
        /// Settings for UV2s.
        /// </summary>
        public GeometryBuilderUVSettings UV2 = new GeometryBuilderUVSettings
        {
            Buffer = Buffer.UV2
        };

        /// <summary>
        /// Settings for Color.
        /// </summary>
        public GeometryBuilderColorSettings Color = new GeometryBuilderColorSettings();

        /// <summary>
        /// Settings for Normals.
        /// </summary>
        public GeometryBuilderNormalSettings Normals = new GeometryBuilderNormalSettings();

        /// <summary>
        /// Settings for Tangents.
        /// </summary>
        public GeometryBuilderTangentSettings Tangents = new GeometryBuilderTangentSettings();
    }
}
