using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

	public Vector3 target;

	[SerializeField]
	private float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime*speed);
	}
}
