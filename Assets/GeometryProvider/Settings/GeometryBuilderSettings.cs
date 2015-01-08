using System;

namespace TheGoldenMule.Geo
{
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
        public GeometryBuilderUVSettings UV1 = new GeometryBuilderUVSettings();
        
        /// <summary>
        /// Settings for UV2s.
        /// </summary>
        public GeometryBuilderUVSettings UV2 = new GeometryBuilderUVSettings();

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
