using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extensions for UnityEngine.Mesh.
/// </summary>
public static class MeshExtensions
{
    /// <summary>
    /// In order to avoid expensive Mesh::Clear() calls, the order in which
    /// the vert and index buffers are assigned to the mesh matters.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="vertices"></param>
    /// <param name="indices"></param>
    public static void Apply(this Mesh @this, ref Vector3[] vertices, ref int[] indices)
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