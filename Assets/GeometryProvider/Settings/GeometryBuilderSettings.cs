using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    [Serializable]
    public class GeometryBuilderTransformSettings
    {
        public Vector3 Scale;
        public Quaternion Rotation;
        public Material Material;
    }

    [Serializable]
    public class GeometryBuilderUVSettings
    {
        public Rect Rect = new Rect(0, 0, 1f, 1f);
    }

    [Serializable]
    public class GeometryBuilderSettings
    {
        public string Name;

        public GeometryBuilderTransformSettings Transform;
        public GeometryBuilderUVSettings UV;
    }
}
