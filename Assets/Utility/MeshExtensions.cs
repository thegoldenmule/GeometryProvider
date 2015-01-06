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
}