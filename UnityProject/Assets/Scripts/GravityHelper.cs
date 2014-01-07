using UnityEngine;
using System.Collections;

public class GravityHelper {

	public static float CalculateGravityForce(float r, float m1, float m2, float g, float distMult)
	{
		return g * ((m1 * m2) / (r * r * distMult * distMult));
	}
}
