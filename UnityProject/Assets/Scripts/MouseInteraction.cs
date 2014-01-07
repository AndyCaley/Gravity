using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class MouseInteraction : MonoBehaviour {

	public int SelectedPrefab = 0;

	public Gravity[] PlanetPrefab;

	private LineRenderer renderer;
	private Gravity CurrentPlanet;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<LineRenderer>();
		renderer.SetVertexCount(0);
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Tab))
		{
			SelectedPrefab++;
			if(SelectedPrefab >= PlanetPrefab.Length)
				SelectedPrefab = 0;

		}

		Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane p = new Plane(Vector3.zero, Vector3.forward, Vector3.right);
		float dist;
		if(p.Raycast(r, out dist))
		{
			Vector3 point = r.GetPoint(dist);

			if(Input.GetMouseButtonDown(0))
			{			
				CurrentPlanet = Instantiate(PlanetPrefab[SelectedPrefab], point, PlanetPrefab[SelectedPrefab].transform.rotation) as Gravity;
				CurrentPlanet.enabled = false;
			}

			if(Input.GetMouseButton(0) && CurrentPlanet != null)
			{
				CurrentPlanet.StartingVelocity = CurrentPlanet.transform.position - point;
				renderer.SetVertexCount(2);
				renderer.SetPosition(0, CurrentPlanet.transform.position);
				renderer.SetPosition(1, point);
			}

			if(Input.GetMouseButtonUp(0))
			{
				renderer.SetVertexCount(0);
				CurrentPlanet.enabled = true;
				CurrentPlanet = null;
			}
		}
	}

	void OnGUI()
	{
		string selectedBody = "";
		switch(SelectedPrefab)
		{
		case 0:
			selectedBody = "Mars";
			break;
		case 1:
			selectedBody = "Moon";
			break;
		case 2:
			selectedBody = "Sun";
			break;
		}
		GUI.Label(new Rect(10, 10, 250, 80), string.Format ("Click and drag to add new body\nCurrently selected body: {0}\n<Tab to switch>", selectedBody));
	}
}
