using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    public abstract class StandardGeometryBuilder : IGeometryBuilder
    {
        public virtual void Initialize()
        {
            // 
        }

        public virtual bool Build(Mesh mesh, GeometryBuilderSettings settings, out string error)
        {
            error = string.Empty;

            // noop

            return true;
        }

        protected virtual void UnshareVerts(Mesh mesh)
        {

        }

        protected virtual void Transform(
            ref Vector3[] inVertices,
            ref Vector3[] outVertices,
            GeometryBuilderSettings settings)
        {
            if (null == inVertices
                || null == outVertices
                || inVertices.Length != outVertices.Length)
            {
                return;
            }

            var transformation = settings.Transform.TRS();
            for (int i = 0, len = inVertices.Length; i < len; i++)
            {
                outVertices[i] = transformation.MultiplyPoint(inVertices[i]);
            }
        }
    }
}
