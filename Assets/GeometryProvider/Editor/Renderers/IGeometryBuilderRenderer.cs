using System;
using UnityEngine;

namespace TheGoldenMule.Geo.Editor
{
    /// <summary>
    /// Interface for custom primitive renderers.
    /// </summary>
    public interface IGeometryBuilderRenderer
    {
        event Action OnCreate;
        event Action OnUpdate;

        bool IsLiveUpdate { get; }
        Transform Selected { get; set; }

        void Draw(GeometryBuilderSettings settings);
    }
}