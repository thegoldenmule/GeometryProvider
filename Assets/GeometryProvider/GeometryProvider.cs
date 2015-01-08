using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

namespace TheGoldenMule.Geo
{
    public static class GeometryProvider
    {
        public static readonly List<IGeometryBuilder> Builders = new List<IGeometryBuilder>();
        public static readonly List<MethodInfo> Factories = new List<MethodInfo>();

        public static void Build(
            Mesh mesh,
            IGeometryBuilder builder,
            GeometryBuilderSettings settings)
        {
            if (null == mesh || null == builder || null == settings)
            {
                return;
            }

            // begin flow
            builder.Start(mesh, settings);

            // create buffers
            int[] triangles = null;
            Vector3[] vertices = null;
            Vector2[] uvs = null;
            Vector2[] uv1s = null;
            Vector2[] uv2s = null;
            Vector3[] normals = null;
            Color[] colors = null;

            // must always have a layout
            builder.Layout(out vertices, out triangles);
            settings.Vertex.Transform(ref vertices, ref triangles);

            // fill other buffers
            if (settings.UV.Enabled)
            {
                builder.UV(out uvs, settings.UV);
                settings.UV.Transform(ref uvs);
            }

            if (settings.UV1.Enabled)
            {
                builder.UV(out uv1s, settings.UV1);
                settings.UV1.Transform(ref uv1s);
            }

            if (settings.UV2.Enabled)
            {
                builder.UV(out uv2s, settings.UV2);
                settings.UV2.Transform(ref uv2s);
            }

            if (settings.Color.Enabled)
            {
                builder.Colors(out colors);
                settings.Color.Transform(ref colors);
            }

            if (settings.Normals.Enabled)
            {
                builder.Normals(out normals);
                settings.Normals.Transform(ref normals);
            }
            
            // set buffers, layout first
            mesh.SetLayout(ref vertices, ref triangles);
            mesh.uv = uvs;
            mesh.uv1 = uv1s;
            mesh.uv2 = uv2s;
            mesh.colors = colors;
            mesh.normals = normals;
            mesh.Optimize();
        }

        static GeometryProvider()
        {
            InitializeBuilders();
        }

        private static void InitializeBuilders()
        {
            var factories = TypeExtensions.MethodsWithAttribute<CustomFactory>();
            var builderTypes = typeof(IGeometryBuilder).Implementors();

            foreach (var builderType in builderTypes)
            {
                if (builderType.IsAbstract)
                {
                    continue;
                }

                try
                {
                    var builder = (IGeometryBuilder) Activator.CreateInstance(builderType);
                    var factory = Factory(builder, factories);

                    Builders.Add(builder);
                    Factories.Add(factory);
                }
                catch
                {
                    Debug.LogError(string.Format(
                        "Could not instantiate IGeometryBuilder {0}. Does it have a public default constructor?",
                        builderType));
                }
            }
        }

        private static MethodInfo Factory(
            IGeometryBuilder builder,
            MethodInfo[] factories)
        {
            foreach (var method in factories)
            {
                var attribute = method.Attribute<CustomFactory>();
                if (null != attribute && attribute.Type == builder.GetType())
                {
                    return method;
                }
            }

            return typeof(GeometryProvider).GetMethod(
                "DefaultFactory",
                BindingFlags.Static | BindingFlags.NonPublic);
        }

        private static GeometryBuilderSettings DefaultFactory()
        {
            return new GeometryBuilderSettings();
        }
    }
}
