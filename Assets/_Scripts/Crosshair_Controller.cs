using UnityEngine;
using System.Collections;

public class Crosshair_Controller : MonoBehaviour {
	
	private GameObject crosshair;

	[SerializeField]
	private Transform shipTransform;

	void Start () {
		crosshair = gameObject;	
	}
	
	// Update is called once per frame
	void Update () {
		crosshair.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 1.35f));
		crosshair.transform.LookAt(shipTransform.position);
	}
}
