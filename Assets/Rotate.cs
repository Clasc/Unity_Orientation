using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	public Vector3 rotationVector;
	public float angle;
    public Vector3 vectorToRotate;
    public Material material;
    private Mesh mesh;
    private MyRigid rigid;
    private MyQuaternion q;
	void Start () {
		Debug.Log("rotation Vector:" + rotationVector.ToString() + "\nAngle: " + angle);
  
		if(vectorToRotate == null)
        {
            vectorToRotate = new Vector3(0, 0, 0);
        }

        mesh = ((MeshFilter)GameObject.CreatePrimitive(PrimitiveType.Cube).GetComponent("MeshFilter")).mesh;
        if(mesh == null)
        {
            Debug.Log("No Mesh in Cube");
        }
        rigid = new MyRigid(mesh);
        angle = angle / 60;
        q = new MyQuaternion(rotationVector, angle);

        rigid.rotate(q);

        Debug.Log("Original Vector: " + vectorToRotate);
        
		Debug.Log("Rotated Point: " + q.rotate(vectorToRotate));

        Graphics.DrawMesh(rigid.getMesh(), Matrix4x4.identity, material, 0);
    }
	
	// Update is called once per frame
	void Update () {

        rigid.rotate(q);


        Graphics.DrawMesh(rigid.getMesh(), Matrix4x4.identity, material, 0);
    }
}
