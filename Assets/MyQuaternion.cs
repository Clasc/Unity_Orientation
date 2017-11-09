using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuaternion{
	private float w;
	private Vector3 vec;

	public MyQuaternion (Vector3 vec, float a){
		a = a /360 * Mathf.PI * 2;

		w = Mathf.Cos(a / 2);

		this.vec =new Vector3(vec.x * Mathf.Sin(a / 2),vec.y * Mathf.Sin(a / 2), vec.z * Mathf.Sin(a / 2));
	}

	public MyQuaternion(MyQuaternion q){
		this.vec = new Vector3 (q.vec.x, q.vec.y, q.vec.z);
		this.w = q.w;
	}

	public MyQuaternion(){
		new MyQuaternion (new Vector3 (1, 0, 0), 0);
	}
	public string toString(){
		return "Quaternion: ( " + w + ", " + vec.x + ", " + vec.y + ", " + vec.z + " )";
	}

	public Vector3 getVector(){
		return vec;
	}
	public float getW(){
		return w;
	}

	public float length(){
		return Mathf.Sqrt (Mathf.Pow (w, 2) + Mathf.Pow (vec.x, 2) + Mathf.Pow (vec.y, 2) + Mathf.Pow (vec.z, 2));
	}

	public MyQuaternion conjugate(){
		MyQuaternion newQ = new MyQuaternion(this);

		newQ.vec.x = -vec.x;
		newQ.vec.y = -vec.y;
		newQ.vec.z = -vec.z;

		return newQ;
	}

	public MyQuaternion invert(){
		MyQuaternion inverted = new MyQuaternion (this);
		MyQuaternion cq = this.conjugate();
		inverted.vec.x = cq.vec.x / this.length ();
		inverted.vec.y = cq.vec.y / this.length ();
		inverted.vec.z = cq.vec.z / this.length ();
		inverted.w = cq.w / this.length ();
		return inverted;
	}



	public static MyQuaternion operator*(MyQuaternion lq, MyQuaternion rq){
		Vector3 v = lq.getVector ();
		Vector3 u = rq.getVector();
		float resultW;
		Vector3 resultVec;

		resultW = lq.w * rq.w - Vector3.Dot (v, u);
		resultVec = lq.w * u + rq.w * v - Vector3.Cross(u,v);


		MyQuaternion m = new MyQuaternion();
		m.w = resultW;
		m.vec = resultVec;

		return m;
	}




	public static MyQuaternion operator*(MyQuaternion lq, Vector3 rv){
		MyQuaternion rop = new MyQuaternion ();
		rop.w = 0;
		rop.vec = rv;
		return lq * rop;
	}


	public Vector3 rotate(Vector3 point){
		return (this * point * this.invert ()).vec;
	}

	public Mesh rotate(Mesh mesh){
		Vector3[] rotated = new Vector3[mesh.vertices.Length];

		for (int i = 0; i < mesh.vertices.Length; i++) {
			rotated [i] = rotate (mesh.vertices [i]);
		}

		mesh.vertices = rotated;
		return mesh;
	}
}
