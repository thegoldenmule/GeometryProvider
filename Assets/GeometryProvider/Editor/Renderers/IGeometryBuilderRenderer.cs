using System;
using UnityEngine;

namespace TheGoldenMule.Geo.Editor
{
    /// <summary>
    /// Interface for custom primitive renderers.
    /// </summary>
    public interface IGeometryBuilderRenderer
    {
        /// <summary>
        /// Called when the renderer requests a primitive to be created.
        /// </summary>
        event Action OnCreate;

        /// <summary>
        /// Called when the renderer requests the Selected primitive be
        /// rebuilt.
        /// </summary>
        event Action OnUpdate;

        /// <summary>
        /// Get/Sets the current selection.
        /// </summary>
        Transform Selected { get; set; }

        /// <summary>
        /// Called by the editor window to draw controls for the builder.
        /// </summary>
        /// <param name="settings"></param>
        void Draw(GeometryBuilderSettings settings);
    }
}