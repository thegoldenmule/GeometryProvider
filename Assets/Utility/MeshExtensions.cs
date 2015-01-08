using System.Collections.Generic;
using UnityEngine;

using TheGoldenMule.Geo;

/// <summary>
/// Extensions for UnityEngine.Mesh.
/// </summary>
public static class MeshExtensions
{
    /// <summary>
    /// In order to avoid expensive Mesh::Clear() calls, the order in which
    /// the vert and index buffers are assigned to the mesh matters.
    /// </summary>
    public static void SetLayout(this Mesh @this, ref Vector3[] vertices, ref int[] indices)
    {
        if (@this.vertexCount > vertices.Length)
        {
            @this.triangles = indices;
            @this.vertices = vertices;
        }
        else
        {
            @this.vertices = vertices;
            @this.triangles = indices;
        }
    }

    /// <summary>
    /// Recalculates tangents. Based on:
    /// 
    /// Lengyel, Eric. "Computing Tangent Space Basis Vectors for an Arbitrary Mesh". Terathon Software 3D Graphics Library, 2001.
    /// http://www.terathon.com/code/tangent.html
    /// </summary>
    /// <param name="this"></param>
    public static void RecalculateTangents(this Mesh @this)
    {
        var vertices = @this.vertices;
        var normals = @this.normals;
        var texcoords = @this.uv;
        var triangles = @this.triangles;

        var vertexCount = @this.vertexCount;
        var triangleCount = triangles.Length / 3;

        var tangents = new Vector4[vertexCount];
        var tan1 = new Vector3[vertexCount];
        var tan2 = new Vector3[vertexCount];

        var tri = 0;

        for (var i = 0; i < triangleCount; i++)
        {
            var i1 = triangles[tri];
            var i2 = triangles[tri + 1];
            var i3 = triangles[tri + 2];

            var v1 = vertices[i1];
            var v2 = vertices[i2];
            var v3 = vertices[i3];

            var w1 = texcoords[i1];
            var w2 = texcoords[i2];
            var w3 = texcoords[i3];

            var x1 = v2.x - v1.x;
            var x2 = v3.x - v1.x;
            var y1 = v2.y - v1.y;
            var y2 = v3.y - v1.y;
            var z1 = v2.z - v1.z;
            var z2 = v3.z - v1.z;

            var s1 = w2.x - w1.x;
            var s2 = w3.x - w1.x;
            var t1 = w2.y - w1.y;
            var t2 = w3.y - w1.y;

            var r = 1.0f / (s1 * t2 - s2 * t1);
            var sdir = new Vector3(
                (t2 * x1 - t1 * x2) * r,
                (t2 * y1 - t1 * y2) * r,
                (t2 * z1 - t1 * z2) * r);
            var tdir = new Vector3(
                (s1 * x2 - s2 * x1) * r,
                (s1 * y2 - s2 * y1) * r,
                (s1 * z2 - s2 * z1) * r);

            tan1[i1] += sdir;
            tan1[i2] += sdir;
            tan1[i3] += sdir;

            tan2[i1] += tdir;
            tan2[i2] += tdir;
            tan2[i3] += tdir;

            tri += 3;
        }

        for (int i = 0; i < vertexCount; i++)
        {

            var normal = normals[i];
            var tangent = tan1[i];

            // Gram-Schmidt orthogonalize
            Vector3.OrthoNormalize(ref normal, ref tangent);

            tangents[i].x = tangent.x;
            tangents[i].y = tangent.y;
            tangents[i].z = tangent.z;

            // Calculate handedness
            tangents[i].w = Vector3.Dot(Vector3.Cross(normal, tangent), tan2[i]) < 0.0f
                ? -1.0f
                : 1.0f;
        }

        @this.tangents = tangents;
    }

    /// <summary>
    /// Subdivides all triangles.
    /// </summary>
    public static void Subdivide(
        ref Vector3[] vertices,
        ref int[] triangles)
    {
        // cache of midpoint indices
        var midpointIndices = new Dictionary<string, int>();

        // create lists instead...
        List<int> indexList = new List<int>(4 * triangles.Length);
        List<Vector3> vertexList = new List<Vector3>(vertices);

        // subdivide each triangle
        for (var i = 0; i < triangles.Length - 2; i += 3)
        {
            // grab indices of triangle
            int i0 = triangles[i];
            int i1 = triangles[i + 1];
            int i2 = triangles[i + 2];

            // calculate new indices
            int m01 = GetMidpointIndex(midpointIndices, vertexList, i0, i1);
            int m12 = GetMidpointIndex(midpointIndices, vertexList, i1, i2);
            int m02 = GetMidpointIndex(midpointIndices, vertexList, i2, i0);

            indexList.AddRange(
                new[] {
				        i0,m01,m02,
				        i1,m12,m01,
				        i2,m02,m12,
				        m02,m01,m12
			        });
        }

        // save
        triangles = indexList.ToArray();
        vertices = vertexList.ToArray();
    }

    /// <summary>
    /// Used by Subdivide method.
    /// </summary>
    private static int GetMidpointIndex(Dictionary<string, int> midpointIndices, List<Vector3> vertices, int i0, int i1)
    {
        // create a key
        string edgeKey = string.Format("{0}_{1}", Mathf.Min(i0, i1), Mathf.Max(i0, i1));

        int midpointIndex = -1;

        // if there is not index already...
        if (!midpointIndices.TryGetValue(edgeKey, out midpointIndex))
        {
            // grab the vertex values
            Vector3 v0 = vertices[i0];
            Vector3 v1 = vertices[i1];

            // calculate
            var midpoint = (v0 + v1) / 2f;

            // save
            if (vertices.Contains(midpoint))
            {
                midpointIndex = vertices.IndexOf(midpoint);
            }
            else
            {
                midpointIndex = vertices.Count;
                vertices.Add(midpoint);
            }
        }

        return midpointIndex;
    }
}