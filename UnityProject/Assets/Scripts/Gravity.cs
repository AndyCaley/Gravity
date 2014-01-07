using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour {

	private GravityController GravityController;
	
	public Vector3 StartingVelocity;


	// Use this for initialization
	void Start () {
		GravityController = GameObject.FindObjectOfType<GravityController>();
		GravityController.Register(this);

		this.rigidbody.AddForce(StartingVelocity, ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		this.transform.Rotate(Vector3.up * Time.deltaTime * 45f);

		foreach(Gravity gravity in GravityController.GravityObjects)
		{
			if(gravity == this)
				continue;

			if(gravity.rigidbody == null)
			{
				Debug.LogWarning("Gravity Object does not have RigidBody");
				continue;
			}

			Vector3 gravityForceVector = this.transform.position - gravity.transform.position;

			float force = GravityHelper.CalculateGravityForce(gravityForceVector.magnitude,
			                                                  this.rigidbody.mass,
			                                                  gravity.rigidbody.mass,
			                                                  GravityController.GravityConstant,
			                                                  GravityController.DistanceMultiplier);

			Vector3 result = force * gravityForceVector.normalized;

			gravity.rigidbody.AddForce(result);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.rigidbody.mass >= this.rigidbody.mass)
		{
			Destroy (this.gameObject);
		}
	}

	void OnDestroy()
	{
		GravityController.UnRegister(this);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawRay(new Ray(this.transform.position, StartingVelocity));
	}
}
