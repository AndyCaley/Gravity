using UnityEngine;
using System.Collections.Generic;

public class GravityController : MonoBehaviour {

	public float GravityConstant;
	public float DistanceMultiplier;

	public IEnumerable<Gravity> GravityObjects;

	private IList<Gravity> _gravityObjects = new List<Gravity>();

	// Use this for initialization
	void Start () {
		GravityObjects = _gravityObjects;
	}

	public void Register(Gravity gravity)
	{
		_gravityObjects.Add(gravity);
	}

	public void UnRegister(Gravity gravity)
	{
		_gravityObjects.Remove(gravity);
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
