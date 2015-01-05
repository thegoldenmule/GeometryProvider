using UnityEngine;

public static class MeshExtensions
{
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