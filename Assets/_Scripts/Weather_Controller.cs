using UnityEngine;
using System.Collections;

public class Weather_Controller : MonoBehaviour {

	[SerializeField]
	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F))
		{
			ps.enableEmission = !ps.enableEmission;
		}
	}
}
