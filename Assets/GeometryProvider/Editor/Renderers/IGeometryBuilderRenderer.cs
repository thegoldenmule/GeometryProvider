using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TheGoldenMule.Geo.Editor
{
    public interface IGeometryBuilderRenderer
    {
        event Action OnCreate;
        event Action OnUpdate;

        bool IsLiveUpdate { get; }
        Transform Selected { get; set; }

        void Draw(GeometryBuilderSettings settings);
    }
}