using UnityEngine;
using System.Collections;

public class GridScript : MonoBehaviour {

	public float Scale;
	public float Offset;

	private Mesh mesh;
	private Vector3[] originalVerts;

	private GravityController _gravityController;

	// Use this for initialization
	void Start () {
		_gravityController = GameObject.FindObjectOfType<GravityController>();
		
		mesh = GetComponent<MeshFilter>().mesh;
		mesh.MarkDynamic();
		originalVerts = new Vector3[mesh.vertices.Length];
		mesh.vertices.CopyTo(originalVerts, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3[] verts = mesh.vertices;

		for(int i = 0; i < verts.Length; i++)
		{
			Vector3 origVertex = originalVerts[i];
			float forceSum = 0;
			foreach(Gravity grav in _gravityController.GravityObjects)
			{
				Vector3 gravInLocalSpace = this.transform.InverseTransformPoint(grav.transform.position);
				float distance = (gravInLocalSpace - origVertex).magnitude;

				float intermediate = GravityHelper.CalculateGravityForce(distance, 1000000, grav.rigidbody.mass, _gravityController.GravityConstant, _gravityController.DistanceMultiplier);
				forceSum += Mathf.Log (intermediate) / Mathf.Log (50);
//				forceSum += Mathf.Log (intermediate) / Mathf.Log (1000); 
			}

			verts[i] = origVertex;
			verts[i].y -= (forceSum * Scale) - Offset;
			verts[i].y = Mathf.Clamp(verts[i].y, -100, 0);
		}

		mesh.vertices = verts;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
	}
}
