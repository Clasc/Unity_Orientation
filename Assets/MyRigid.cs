using UnityEngine;

public class MyRigid
{
    private Mesh mesh;
    public MyRigid(Mesh mesh)
    {
        this.mesh = mesh;
    }

    public Mesh getMesh()
    {
        return mesh;
    }

    public void rotate(MyQuaternion q)
    {
        Vector3[] verts = mesh.vertices;
        Vector3[] rotated = new Vector3[verts.Length];
        for(int i = 0; i < verts.Length; i++)
        {
            rotated[i] = q.rotate(verts[i]);
        }
        mesh.vertices = rotated;
    }
}